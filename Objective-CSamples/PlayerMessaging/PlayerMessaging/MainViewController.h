//
//  MainViewController.h
//  PlayerMessaging
//
//  Created by Robert Henning on 31/10/2016.
//  Copyright © 2016 Chilli Technologies. All rights reserved.
//

#import <UIKit/UIKit.h>

#import <FBSDKLoginKit/FBSDKLoginKit.h>

@interface MainViewController : UIViewController <FBSDKLoginButtonDelegate>

@property (weak, nonatomic) IBOutlet UILabel* balanceLabel;
@property (weak, nonatomic) IBOutlet UIButton* sendButton;
@property (weak, nonatomic) IBOutlet UIButton* inboxButton;
@property (weak, nonatomic) IBOutlet UILabel* loggedInAsLabel;
@property (weak, nonatomic) IBOutlet UILabel* facebookPosition;
@property FBSDKLoginButton* facebookLoginButton;

@end
