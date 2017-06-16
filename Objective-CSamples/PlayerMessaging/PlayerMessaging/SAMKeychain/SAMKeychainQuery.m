//
//  SAMKeychainQuery.m
//  SAMKeychain
//
//  Created by Caleb Davenport on 3/19/13.
//  Copyright (c) 2013-2014 Sam Soffes. All rights reserved.
//

#import "SAMKeychainQuery.h"
#import "SAMKeychain.h"

@implementation SAMKeychainQuery

@synthesize account = _account;
@synthesize service = _service;
@synthesize label = _label;
@synthesize passwordData = _passwordData;

#ifdef SAMKEYCHAIN_ACCESS_GROUP_AVAILABLE
@synthesize accessGroup = _accessGroup;
#endif

#ifdef SAMKEYCHAIN_SYNCHRONIZATION_AVAILABLE
@synthesize synchronizationMode = _synchronizationMode;
#endif

#pragma mark - Public

- (BOOL)save:(NSError *__autoreleasing *)error {
	OSStatus status = SAMKeychainErrorBadArguments;
	if (!self.service || !self.account || !self.passwordData) {
		if (error) {
			*error = [[self class] errorWithCode:status];
		}
		return NO;
	}
	NSMutableDictionary *query = nil;
	NSMutableDictionary * searchQuery = [self query];
	status = SecItemCopyMatching((__bridge CFDictionaryRef)searchQuery, nil);
	if (status == errSecSuccess) {//item already exists, update it!
		query = [[NSMutableDictionary alloc]init];
		[query setObject:self.passwordData forKey:(__bridge id)kSecValueData];
#if __IPHONE_4_0 && TARGET_OS_IPHONE
		CFTypeRef accessibilityType = [SAMKeychain accessibilityType];
		if (accessibilityType) {
			[query setObject:(__bridge id)accessibilityType forKey:(__bridge id)kSecAttrAccessible];
		}
#endif
		status = SecItemUpdate((__bridge CFDictionaryRef)(searchQuery), (__bridge CFDictionaryRef)(query));
	}else if(status == errSecItemNotFound){//item not found, create it!
		query = [self query];
		if (self.label) {
			[query setObject:self.label forKey:(__bridge id)kSecAttrLabel];
		}
		[query setObject:self.passwordData forKey:(__bridge id)kSecValueData];
#if __IPHONE_4_0 && TARGET_OS_IPHONE
		CFTypeRef accessibilityType = [SAMKeychain accessibilityType];
		if (accessibilityType) {
			[query setObject:(__bridge id)accessibilityType forKey:(__bridge id)kSecAttrAccessible];
		}
#endif
		status = SecItemAdd((__bridge CFDictionaryRef)query, NULL);
	}
	if (status != errSecSuccess && error != NULL) {
		*error = [[self class] errorWithCode:status];
	}
	return (status == errSecSuccess);}


- (BOOL)deleteItem:(NSError *__autoreleasing *)error {
	OSStatus status = SAMKeychainErrorBadArguments;
	if (!self.service || !self.account) {
		if (error) {
			*error = [[self class] errorWithCode:status];
		}
		return NO;
	}

	NSMutableDictionary *query = [self query];
#if TARGET_OS_IPHONE
	status = SecItemDelete((__bridge CFDictionaryRef)query);
#else
	// On Mac OS, SecItemDelete will not delete a key created in a different
	// app, nor in a different version of the same app.
	//
	// To replicate the issue, save a password, change to the code and
	// rebuild the app, and then attempt to delete that password.
	//
	// This was true in OS X 10.6 and probably later versions as well.
	//
	// Work around it by using SecItemCopyMatching and SecKeychainItemDelete.
	CFTypeRef result = NULL;
	[query setObject:@YES forKey:(__bridge id)kSecReturnRef];
	status = SecItemCopyMatching((__bridge CFDictionaryRef)query, &result);
	if (status == errSecSuccess) {
		status = SecKeychainItemDelete((SecKeychainItemRef)result);
		CFRelease(result);
	}
#endif

	if (status != errSecSuccess && error != NULL) {
		*error = [[self class] errorWithCode:status];
	}

	return (status == errSecSuccess);
}


- (nullable NSArray *)fetchAll:(NSError *__autoreleasing *)error {
	NSMutableDictionary *query = [self query];
	[query setObject:@YES forKey:(__bridge id)kSecReturnAttributes];
	[query setObject:(__bridge id)kSecMatchLimitAll forKey:(__bridge id)kSecMatchLimit];
#if __IPHONE_4_0 && TARGET_OS_IPHONE
	CFTypeRef accessibilityType = [SAMKeychain accessibilityType];
	if (accessibilityType) {
		[query setObject:(__bridge id)accessibilityType forKey:(__bridge id)kSecAttrAccessible];
	}
#endif

	CFTypeRef result = NULL;
	OSStatus status = SecItemCopyMatching((__bridge CFDictionaryRef)query, &result);
	if (status != errSecSuccess && error != NULL) {
		*error = [[self class] errorWithCode:status];
		return nil;
	}

	return (__bridge_transfer NSArray *)result;
}


- (BOOL)fetch:(NSError *__autoreleasing *)error {
	OSStatus status = SAMKeychainErrorBadArguments;
	if (!self.service || !self.account) {
		if (error) {
			*error = [[self class] errorWithCode:status];
		}
		return NO;
	}

	CFTypeRef result = NULL;
	NSMutableDictionary *query = [self query];
	[query setObject:@YES forKey:(__bridge id)kSecReturnData];
	[query setObject:(__bridge id)kSecMatchLimitOne forKey:(__bridge id)kSecMatchLimit];
	status = SecItemCopyMatching((__bridge CFDictionaryRef)query, &result);

	if (status != errSecSuccess) {
		if (error) {
			*error = [[self class] errorWithCode:status];
		}
		return NO;
	}

	self.passwordData = (__bridge_transfer NSData *)result;
	return YES;
}


#pragma mark - Accessors

- (void)setPasswordObject:(id<NSCoding>)object {
	self.passwordData = [NSKeyedArchiver archivedDataWithRootObject:object];
}


- (id<NSCoding>)passwordObject {
	if ([self.passwordData length]) {
		return [NSKeyedUnarchiver unarchiveObjectWithData:self.passwordData];
	}
	return nil;
}


- (void)setPassword:(NSString *)password {
	self.passwordData = [password dataUsingEncoding:NSUTF8StringEncoding];
}


- (NSString *)password {
	if ([self.passwordData length]) {
		return [[NSString alloc] initWithData:self.passwordData encoding:NSUTF8StringEncoding];
	}
	return nil;
}


#pragma mark - Synchronization Status

#ifdef SAMKEYCHAIN_SYNCHRONIZATION_AVAILABLE
+ (BOOL)isSynchronizationAvailable {
#if TARGET_OS_IPHONE
	// Apple suggested way to check for 7.0 at runtime
	// https://developer.apple.com/library/ios/documentation/userexperience/conceptual/transitionguide/SupportingEarlieriOS.html
	return floor(NSFoundationVersionNumber) > NSFoundationVersionNumber_iOS_6_1;
#else
	return floor(NSFoundationVersionNumber) > NSFoundationVersionNumber10_8_4;
#endif
}
#endif


#pragma mark - Private

- (NSMutableDictionary *)query {
	NSMutableDictionary *dictionary = [NSMutableDictionary dictionaryWithCapacity:3];
	[dictionary setObject:(__bridge id)kSecClassGenericPassword forKey:(__bridge id)kSecClass];

	if (self.service) {
		[dictionary setObject:self.service forKey:(__bridge id)kSecAttrService];
	}

	if (self.account) {
		[dictionary setObject:self.account forKey:(__bridge id)kSecAttrAccount];
	}

#ifdef SAMKEYCHAIN_ACCESS_GROUP_AVAILABLE
#if !TARGET_IPHONE_SIMULATOR
	if (self.accessGroup) {
		[dictionary setObject:self.accessGroup forKey:(__bridge id)kSecAttrAccessGroup];
	}
#endif
#endif

#ifdef SAMKEYCHAIN_SYNCHRONIZATION_AVAILABLE
	if ([[self class] isSynchronizationAvailable]) {
		id value;

		switch (self.synchronizationMode) {
			case SAMKeychainQuerySynchronizationModeNo: {
			  value = @NO;
			  break;
			}
			case SAMKeychainQuerySynchronizationModeYes: {
			  value = @YES;
			  break;
			}
			case SAMKeychainQuerySynchronizationModeAny: {
			  value = (__bridge id)(kSecAttrSynchronizableAny);
			  break;
			}
		}

		[dictionary setObject:value forKey:(__bridge id)(kSecAttrSynchronizable)];
	}
#endif

	return dictionary;
}


+ (NSError *)errorWithCode:(OSStatus) code {
    /* There is a bug with the line
     
         resourcesBundle = [NSBundle bundleWithURL:url];
     
       as the url is nil. We don't need this so the fix is to remove it
       and replace NSLocalizedStringFromTableInBundle with NSLocalizedStringFromTable
       as suggestged by this post https://github.com/soffes/SAMKeychain/issues/139
     */
    
//	static dispatch_once_t onceToken;
//	static NSBundle *resourcesBundle = nil;
//	dispatch_once(&onceToken, ^{
//		NSURL *url = [[NSBundle bundleForClass:[SAMKeychainQuery class]] URLForResource:@"SAMKeychain" withExtension:@"bundle"];
//		resourcesBundle = [NSBundle bundleWithURL:url];
//	});
	
	NSString *message = nil;
	switch (code) {
		case errSecSuccess: return nil;
		case SAMKeychainErrorBadArguments: message = NSLocalizedStringFromTable(@"SAMKeychainErrorBadArguments", @"SAMKeychain", nil); break;

#if TARGET_OS_IPHONE
		case errSecUnimplemented: {
			message = NSLocalizedStringFromTable(@"errSecUnimplemented", @"SAMKeychain", nil);
			break;
		}
		case errSecParam: {
			message = NSLocalizedStringFromTable(@"errSecParam", @"SAMKeychain", nil);
			break;
		}
		case errSecAllocate: {
			message = NSLocalizedStringFromTable(@"errSecAllocate", @"SAMKeychain", nil);
			break;
		}
		case errSecNotAvailable: {
			message = NSLocalizedStringFromTable(@"errSecNotAvailable", @"SAMKeychain", nil);
			break;
		}
		case errSecDuplicateItem: {
			message = NSLocalizedStringFromTable(@"errSecDuplicateItem", @"SAMKeychain", nil);
			break;
		}
		case errSecItemNotFound: {
			message = NSLocalizedStringFromTable(@"errSecItemNotFound", @"SAMKeychain", nil);
			break;
		}
		case errSecInteractionNotAllowed: {
			message = NSLocalizedStringFromTable(@"errSecInteractionNotAllowed", @"SAMKeychain", nil);
			break;
		}
		case errSecDecode: {
			message = NSLocalizedStringFromTable(@"errSecDecode", @"SAMKeychain", nil);
			break;
		}
		case errSecAuthFailed: {
			message = NSLocalizedStringFromTable(@"errSecAuthFailed", @"SAMKeychain", nil);
			break;
		}
		default: {
			message = NSLocalizedStringFromTable(@"errSecDefault", @"SAMKeychain", nil);
		}
#else
		default:
			message = (__bridge_transfer NSString *)SecCopyErrorMessageString(code, NULL);
#endif
	}

	NSDictionary *userInfo = nil;
	if (message) {
		userInfo = @{ NSLocalizedDescriptionKey : message };
	}
	return [NSError errorWithDomain:kSAMKeychainErrorDomain code:code userInfo:userInfo];
}

@end
