//
//  This file was auto-generated using the ChilliConnect SDK Generator.
//
//  The MIT License (MIT)
//
//  Copyright (c) 2015 Tag Games Ltd
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//

@import Foundation;

#import "ForwardDeclarations.h"

NS_ASSUME_NONNULL_BEGIN

/*!
 A container for information on a DLC package file.

 This is immutable after construction and is therefore thread safe.
 */
@interface CCDlcPackageFile : NSObject

/// The file name.
@property (readonly) NSString *name;
	
/// The file location within the package.
@property (readonly) NSString *location;
	
/// The file size in bytes.
@property (readonly) int32_t size;
	
/// The file SHA1 checksum.
@property (readonly) NSString *checksum;

/*!
 A convenience factory method for creating new instances of CCDlcPackageFile
 with the given properties.
 
 @param name The file name.
 @param location The file location within the package.
 @param size The file size in bytes.
 @param checksum The file SHA1 checksum.

 @return The new instance.
 */
+ (instancetype)dlcPackageFileWithName:(NSString *)name location:(NSString *)location size:(int32_t)size checksum:(NSString *)checksum;

/*!
 Convenience factory method for creating new instances of CCDlcPackageFile
 from the contents of the given Json dictionary.
 
 @param dictionary The properties of the object in dictionary form. Typically this
        is created from Json.
 
 @return The new instance.
 */
+ (instancetype)dlcPackageFileWithJson:(NSDictionary *)dictionary;

/*!
 Default init method cannot be called for this class.
 */
-(instancetype) __unavailable init;

/*!
 Initialises with the given properties.
 
 @param name The file name.
 @param location The file location within the package.
 @param size The file size in bytes.
 @param checksum The file SHA1 checksum.

 @return The initialised object.
 */
- (instancetype)initWithName:(NSString *)name location:(NSString *)location size:(int32_t)size checksum:(NSString *)checksum NS_DESIGNATED_INITIALIZER;

/*!
 Initialise with the contents of the given dictionary.
 
 @param dictionary The properties of the object in dictionary form. Typically this
        is created from Json.
 
 @return The initialised object.
 */
- (instancetype)initWithDictionary:(NSDictionary *)dictionary NS_DESIGNATED_INITIALIZER;

/*!
 Serialises all properties. The output will be a dictionary containing the objects 
 properties in a form that can easily be converted to Json. 
 
 @return The serialised object in dictionary form. 
 */
 - (NSDictionary *)serialise;
 
@end

NS_ASSUME_NONNULL_END
