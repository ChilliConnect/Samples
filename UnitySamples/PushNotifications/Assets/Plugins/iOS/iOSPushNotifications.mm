//------------------------------------------------------------------
///
///
///
//------------------------------------------------------------------

#include "iOSPushNotifications.h"

NSString* const kUnityDidRegisterForRemoteNotificationsWithDeviceToken = @"kUnityDidRegisterForRemoteNotificationsWithDeviceToken";
NSString* const kUnityDidFailToRegisterForRemoteNotificationsWithError = @"kUnityDidFailToRegisterForRemoteNotificationsWithError";

@implementation iOSPushNotifications

 __strong iOSPushNotifications *sharedInstance;

//------------------------------------------------------------------
///
/// Similar to an awake or a constructor.
///
//------------------------------------------------------------------
+ (void)load
{
    //sharedInstance = [[self alloc] init];
}

- (id)init
{
    printf("iOSPushNotifications : init");

    if (!(self = [super init]))
    {
        printf("iOSPushNotifications : init - FAILED!");
        return nil;
    }
    
    NSNotificationCenter *nc = [NSNotificationCenter defaultCenter];
    [nc addObserver:self
           selector:@selector(didRegisterForRemoteNotificationsWithDeviceToken:)
            name:kUnityDidRegisterForRemoteNotificationsWithDeviceToken
            object:nil];
    
    [nc addObserver:self
           selector:@selector(didFailToRegisterForRemoteNotificationsWithError:)
               name:kUnityDidFailToRegisterForRemoteNotificationsWithError
             object:nil];
    
    printf("iOSPushNotifications : init - SUCCESS!");
    
    return self;
}

//------------------------------------------------------------------
///
///
///
//------------------------------------------------------------------
+ (void) RegisterForRemoteNotifications
{
    printf("iOSPushNotifications : RegisterForRemoteNotifications");
    
    UIApplication * application = [UIApplication sharedApplication];
    
    // None of the code should even be compiled unless the Base SDK is iOS 8.0 or later
    #if __IPHONE_OS_VERSION_MAX_ALLOWED >= 80000
        // The following line must only run under iOS 8. This runtime check prevents
        // it from running if it doesn't exist (such as running under iOS 7 or earlier).
        if ([application respondsToSelector:@selector(registerUserNotificationSettings:)])
        {
            [application registerUserNotificationSettings:[UIUserNotificationSettings settingsForTypes:UIUserNotificationTypeAlert|UIUserNotificationTypeBadge|UIUserNotificationTypeSound categories:nil]];
            
            printf("iOSPushNotifications : RegisterForRemoteNotifications - SUCCESS!");
        }
    #endif
}
//------------------------------------------------------------------
///
///
///
//------------------------------------------------------------------
//+ void application(application: UIApplication, didRegisterUserNotificationSettings notificationSettings: UIUserNotificationSettings)
//{
//    printf("iOSPushNotifications : didRegisterUserNotificationSettings");
//    
//    if (notificationSettings.types != .None)
//    {
//        [self RegisterForRemoteNotifications];
//    }
//}
//------------------------------------------------------------------
///
///
///
//------------------------------------------------------------------
- (void) didRegisterForRemoteNotificationsWithDeviceToken:(NSNotification *) notification
{
    printf("\n iOSPushNotifications : didRegisterForRemoteNotificationsWithDeviceToken");
    
    NSString *myString = [[NSString alloc] initWithData:[notification object ] encoding:NSUTF8StringEncoding];
    
    NSLog(@"\n iOSPushNotifications : didRegisterForRemoteNotificationsWithDeviceToken - Token = %@", myString);
}
//------------------------------------------------------------------
///
///
///
//------------------------------------------------------------------
- (void) didFailToRegisterForRemoteNotificationsWithError:(NSNotification *) notification
{
    printf("\n iOSPushNotifications : didFailToRegisterForRemoteNotificationsWithError");
    //NSLog(@"iOSPushNotifications : Error in registration. Error: %@", err);
}

@end

extern "C"
{
    #include "iOSPushNotifications.h"
    
    void _RegisterForRemoteNotifications()
    {
        [iOSPushNotifications RegisterForRemoteNotifications];
    }
}
