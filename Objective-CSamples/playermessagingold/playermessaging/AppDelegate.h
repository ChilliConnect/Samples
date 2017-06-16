//
//  AppDelegate.h
//  PlayerMessaging
//
//  Created by Robert Henning on 01/09/2016.
//  Copyright © 2016 Chilli Technologies. All rights reserved.
//

#import <UIKit/UIKit.h>

@class ChilliConnectController;

@interface AppDelegate : UIResponder <UIApplicationDelegate>{
}

@property (strong, nonatomic) UIWindow* window;

- (ChilliConnectController*) getChilliConnectController;

@end
