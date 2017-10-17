//
//  LaunchViewController.m
//  PlayerMessaging
//
//  Created by Robert Henning on 04/11/2016.
//  Copyright © 2016 Chilli Technologies. All rights reserved.
//

#import "LaunchViewController.h"

#import "FBViewController.h"

#import <FBSDKCoreKit/FBSDKCoreKit.h>
#import <FBSDKLoginKit/FBSDKLoginKit.h>

@interface LaunchViewController ()

@end

@implementation LaunchViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    self.view.backgroundColor = UIColor.whiteColor;
}

- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    if([FBSDKAccessToken currentAccessToken])
    {
        NSLog(@"FB App ID %@", [FBSDKAccessToken currentAccessToken].appID);
        NSLog(@"FB Access Token %@", [FBSDKAccessToken currentAccessToken].tokenString);
        [self performSegueWithIdentifier:@"CCMainSegue" sender:self];
    }
    else
    {
        [self performSegueWithIdentifier:@"FBLoginSegue" sender:self];
    }
}

@end
