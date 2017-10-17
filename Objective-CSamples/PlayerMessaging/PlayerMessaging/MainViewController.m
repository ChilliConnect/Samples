//
//  InboxViewController.m
//  PlayerMessaging
//
//  Created by Robert Henning on 31/10/2016.
//  Copyright © 2016 Chilli Technologies. All rights reserved.
//

#import "MainViewController.h"

#import "AppDelegate.h"
#import "ChilliConnectController.h"
#import "FBViewController.h"

#import <FBSDKCoreKit/FBSDKCoreKit.h>
#import <FBSDKLoginKit/FBSDKLoginKit.h>

@interface MainViewController ()

@end

@implementation MainViewController

- (void) viewDidLoad
{
    [super viewDidLoad];
}

- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    if([FBSDKAccessToken currentAccessToken])
    {
        if(!self.facebookLoginButton)
        {
            self.facebookLoginButton = [[FBSDKLoginButton alloc] init];
            self.facebookLoginButton.readPermissions = @[@"public_profile", @"email", @"user_friends"];
            self.facebookLoginButton.center = self.facebookPosition.center;
            self.facebookLoginButton.delegate = self;
            [self.view addSubview:self.facebookLoginButton];
        }
        
        [self login];
    }
    else
    {
        AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
        ChilliConnectController* chilliConnectController = [app getChilliConnectController];
        
        if([chilliConnectController getLoginState] == kLoggedOut)
        {
            NSLog(@"We do not have a Facebook access token and we are not logged in.");
            [self performSegueWithIdentifier:@"FBLoginSegue" sender:self];
        }
    }
}

- (void) didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void) login
{
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    ChilliConnectController* chilliConnectController = [app getChilliConnectController];
    
    if([chilliConnectController hasCredentials])
    {
        if([chilliConnectController getLoginState] == kLoggedOut)
        {
            ChilliConnectControllerCallback loginUsingFacebookCallback = ^(BOOL success, NSString* errorDescription)
            {
                if(success)
                {
                    NSLog(@"Logged in using Facebook.");
                    [self updateLoggedInAsLabel:chilliConnectController];
                    [self updatePlayerBalance:chilliConnectController];
                    [self show:YES];
                }
                else
                {
                    NSLog(@"%@",errorDescription);
                }
            };
            // You want to login here, try with Facebook and if it fails then loading with saved CC credentials.
            [chilliConnectController loginUsingFacebook:loginUsingFacebookCallback];
        }
        else if([chilliConnectController getLoginState] == kLoggedInFacebook)
        {
            [self updateLoggedInAsLabel:chilliConnectController];
            [self updatePlayerBalance:chilliConnectController];
            [self show:YES];
        }
    }
    else
    {
        // Try to login using Facebook. If successful, login otherwise create a new user.
        [self tryLoginUsingFacebook:chilliConnectController];
    }
}

/* Attempts to link the currently logged in Chilli Connect account to a Facebook account. */
- (void) linkChilliConnectToFacebook:(ChilliConnectController*)chilliConnectController
{
    // Now that we have logged we should try and link our Facebook account.
    ChilliConnectControllerCallback linkFacebookAccountCallback = ^(BOOL success, NSString* errorDescription)
    {
        if(success)
        {
            NSLog(@"Account successfully linked to Facebook");
            [self login];
        }
        else
        {
            NSLog(@"%@",errorDescription);
        }
    };
    [chilliConnectController linkFacebookAccount:linkFacebookAccountCallback];
}

/* Attempts to login using saved Chilli Connect credentials. */
- (void) loginChilliConnectUser:(ChilliConnectController*)chilliConnectController
{
    // Now that we have created an account, we need to login.
    ChilliConnectControllerCallback loginCallback = ^(BOOL success, NSString* errorDescription)
    {
        if(success)
        {
            NSLog(@"Logged in succesfully.");
            [self linkChilliConnectToFacebook:chilliConnectController];
        }
        else
        {
            NSLog(@"%@",errorDescription);
        }
    };
    [chilliConnectController login:loginCallback];
}

/* Attempts to login using Facebook. If this Facebook user has logged in before then we retrieve the associated Chilli Connect credentials otherwise we create a new user. */
- (void) tryLoginUsingFacebook:(ChilliConnectController*)chilliConnectController
{
    ChilliConnectControllerCallback loginUsingFacebookCallback = ^(BOOL success, NSString* errorDescrption)
    {
        if(success)
        {
            [self login];
        }
        else
        {
            NSLog(@"%@",errorDescrption);
            [self createNewUser:chilliConnectController];
        }
    };
    [chilliConnectController loginUsingFacebook:loginUsingFacebookCallback];
}

/* Attempts to create a new user. If successful we will try to login. */
- (void) createNewUser:(ChilliConnectController*)chilliConnectController
{
    // We do not have any save credentials so we need to create a new account and then login.
    ChilliConnectControllerCallback createNewUserCallback = ^(BOOL success, NSString* errorDescription)
    {
        if(success)
        {
            NSLog(@"Account successfully created");
            [self loginChilliConnectUser:chilliConnectController];
        }
        else
        {
            NSLog(@"%@",errorDescription);
        }
    };
    [chilliConnectController createNewUser:createNewUserCallback];
}

/* Show or hide the contents of this view */
- (void) show:(BOOL)show
{
    self.balanceLabel.hidden = !show;
    self.inboxButton.hidden = !show;
    self.sendButton.hidden = !show;
    self.loggedInAsLabel.hidden = !show;
}

/* Update logged in player name */
- (void) updateLoggedInAsLabel:(ChilliConnectController*)chilliConnectController
{
    NSString* loggedInText = @"Logged In As: ";
    loggedInText = [loggedInText stringByAppendingString:[chilliConnectController getMyFacebookName]];
    self.loggedInAsLabel.text = loggedInText;
}

/* Request the current balances for the current user */
- (void) updatePlayerBalance:(ChilliConnectController*)chilliConnectController
{
    ChilliConnectControllerBalancesCallback callback = ^(BOOL success, NSString* errorDescription, NSArray* balances)
    {
        CCCurrencyBalance* balance = [balances objectAtIndex:0];
        self.balanceLabel.text = [NSString stringWithFormat:@"%d",balance.balance];
    };
    [chilliConnectController getCoinsBalances:callback];
}

#pragma mark - Facebook Delegate

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
        [self login];
    }
}

- (void)loginButtonDidLogOut:(FBSDKLoginButton*)loginButton
{
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    ChilliConnectController* chilliConnectController = [app getChilliConnectController];
    
    [chilliConnectController deleteCredentials];
    [self show:NO];
}

- (BOOL)loginButtonWillLogin:(FBSDKLoginButton *)loginButton
{
    NSLog(@"Facebook will login");
    return YES;
}

@end
