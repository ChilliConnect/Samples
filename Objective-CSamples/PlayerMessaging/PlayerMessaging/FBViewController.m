//
//  FBViewController.m
//  PlayerMessaging
//
//  Created by Robert Henning on 01/09/2016.
//  Copyright © 2016 Chilli Technologies. All rights reserved.
//

#import "FBViewController.h"

@interface FBViewController ()

@end

@implementation FBViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    if([FBSDKAccessToken currentAccessToken])
    {
        NSLog(@"FB App ID %@", [FBSDKAccessToken currentAccessToken].appID);
        NSLog(@"FB Access Token %@", [FBSDKAccessToken currentAccessToken].tokenString);
        [self dismissViewControllerAnimated:YES completion:nil];
    }
    
    FBSDKLoginButton* loginButton = [[FBSDKLoginButton alloc] init];
    loginButton.readPermissions = @[@"public_profile", @"email", @"user_friends"];
    // Optional: Place the button in the center of your view.
    loginButton.center = self.view.center;
    loginButton.delegate = self;
    [self.view addSubview:loginButton];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)loginButton:(FBSDKLoginButton*)loginButton didCompleteWithResult:(FBSDKLoginManagerLoginResult*)result error:(NSError*)error
{
    if(error)
    {
        NSLog(@"Facebook login Failed: %@ Code:%ld", error.localizedDescription, (long)error.code);
    }
    if(result.isCancelled)
    {
        NSLog(@"Facebook login Cancelled.");
    }
    else
    {
         NSLog(@"Facebook login successful.");
        loginButton.delegate = nil;
        [self dismissViewControllerAnimated:YES completion:nil];
    }
}

- (void)loginButtonDidLogOut:(FBSDKLoginButton*)loginButton
{
    NSLog(@"Facebook logout");
}

- (BOOL)loginButtonWillLogin:(FBSDKLoginButton *)loginButton
{
    NSLog(@"Facebook will login");
    return YES;
}

@end
