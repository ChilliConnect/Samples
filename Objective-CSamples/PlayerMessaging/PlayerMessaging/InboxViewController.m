//
//  InboxViewController.m
//  PlayerMessaging
//
//  Created by Robert Henning on 11/11/2016.
//  Copyright © 2016 Chilli Technologies. All rights reserved.
//

#import "InboxViewController.h"

#import "AppDelegate.h"
#import "ChilliConnectController.h"
#import "InboxViewCell.h"

@interface InboxViewController ()

@end

@implementation InboxViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    NSLog(@"InboxViewController");
    
    UIRefreshControl* refreshControl = [[UIRefreshControl alloc] init];
    [refreshControl addTarget:self action:@selector(refresh) forControlEvents:UIControlEventValueChanged];
    self.refreshControl = refreshControl;
    self.selectedRow = -1;
    [self refresh];
}

- (NSInteger)numberOfSectionsInTableView:(UITableView*)tableView
{
    // Return the number of sections.
    return 1;
}

- (NSInteger)tableView:(UITableView*)tableView numberOfRowsInSection:(NSInteger)section
{
    // Return the number of rows in the section.
    NSUInteger value = [self.messages count];
    NSLog(@"Got %ld messages.",(unsigned long)value);
    return value;
}

- (UITableViewCell*)tableView:(UITableView*)tableView cellForRowAtIndexPath:(NSIndexPath*)indexPath
{
    static NSString* cellIdentifier = @"inboxTableCell";
    
    InboxViewCell* cell = [tableView dequeueReusableCellWithIdentifier:cellIdentifier];
    if(cell == nil)
    {
        cell = [[InboxViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:cellIdentifier];
    }
    
    // Find matching Facebook friend for this message
    CCMessage* message = [self.messages objectAtIndex:[indexPath row]];
    
    for(CCFacebookPlayerWithProfileImage* friend in self.friends)
    {
        if([friend.chilliConnectId isEqualToString:message.from.chilliConnectId])
        {
            [cell.message setText:friend.facebookName];
            
            NSData* imageData = [[NSData alloc] initWithContentsOfURL:[NSURL URLWithString:friend.facebookProfileImage]];
            [cell.imageView setImage:[UIImage imageWithData:imageData]];
        }
    }
    
    return cell;
}

- (void)tableView:(UITableView*)tableView didSelectRowAtIndexPath:(NSIndexPath*)indexPath
{
    if(indexPath.row != self.selectedRow && self.selectedRow != -1)
    {
        // hide options or whatever you want to do
        self.selectedRow = -1;
    }
    else
    {
        // show your options or other things
        self.selectedRow = indexPath.row;
        [self showAlert:indexPath.row];
    }
}

- (void) refresh
{
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    ChilliConnectController* chilliConnectController = [app getChilliConnectController];
    NSAssert(chilliConnectController, @"Chilli Connect controller does not exist.");
    
    if([chilliConnectController hasFacebookFriends])
    {
        self.friends = chilliConnectController.getCachedFacebookFriends;
        [self refreshMessages:chilliConnectController];
    }
    else
    {
        ChilliConnectControllerCallback fetchFacebookFriendsCallback = ^(BOOL success, NSString* errorDescription)
        {
            if(success)
            {
                self.friends = chilliConnectController.getCachedFacebookFriends;
                [self refreshMessages:chilliConnectController];
            }
            else
            {
                NSLog(@"%@",errorDescription);
            }
        };
        [chilliConnectController fetchFacebookFriends:fetchFacebookFriendsCallback];
    }
}

- (void) refreshMessages:(ChilliConnectController*)chilliConnectController
{
    ChilliConnectControllerGetMessagesCallback getMessagesCallback = ^(BOOL success, NSString* errorDescription, NSArray* messages)
    {
        if(success)
        {
            self.messages = messages;
            [self.tableView reloadData];
            [self.refreshControl endRefreshing];
        }
        else
        {
            NSLog(@"%@",errorDescription);
        }
    };
    [chilliConnectController getMessages:getMessagesCallback];
}

- (void) showAlert:(NSInteger)messageIndex
{
    if(messageIndex < 0)
    {
        return;
    }
    
    CCMessage* message = [self.messages objectAtIndex:messageIndex];
    CCMessageRewardCurrency* messageReward = [message.rewards.currencies objectAtIndex:0];
    int amount = messageReward.amount;
    
    NSString* alertMessage = [NSString stringWithFormat:@"Do you want to redeem %d coins?", amount];
    UIAlertController* alert = [UIAlertController alertControllerWithTitle:@"Redeem Gift?" message:alertMessage preferredStyle:UIAlertControllerStyleAlert];
    
    UIAlertAction* sendAction = [UIAlertAction actionWithTitle:@"Redeem" style:UIAlertActionStyleDefault handler:^(UIAlertAction* action)
                                 {
                                     NSLog(@"Redeeming %d",amount);
                                     [self redeemRewardForMessageAtIndex:messageIndex];
                                 }];
    
    UIAlertAction* dismissButton = [UIAlertAction actionWithTitle:@"Dismiss" style:UIAlertActionStyleDefault handler:nil];
    
    [alert addAction:dismissButton];
    [alert addAction:sendAction];
    [self presentViewController:alert animated:YES completion:nil];
}

- (void) redeemRewardForMessageAtIndex:(NSInteger)messageIndex
{
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    ChilliConnectController* chilliConnectController = [app getChilliConnectController];
    NSAssert(chilliConnectController, @"Chilli Connect controller does not exist.");
    
    CCMessage* message = [self.messages objectAtIndex:messageIndex];
    ChilliConnectControllerCallback redeemMessageRewardCallback = ^(BOOL success, NSString* errorDescription)
    {
        if(success)
        {
            // Coins have been rewarded, so update our view as this message has been processed.
            [self refresh];
        }
        else
        {
            NSLog(@"%@",errorDescription);
        }
        [self showGiftRedeemedSuccessAlert:success];
    };
    [chilliConnectController redeemMessageReward:message.messageId callback:redeemMessageRewardCallback];
}

- (void) showGiftRedeemedSuccessAlert:(BOOL)success
{
    NSString* title = @"";
    NSString* message = @"";
    if(success)
    {
        title = [title stringByAppendingString:@"Gift Redeemed"];
        message = [message stringByAppendingString:@"Your gift was redeemed."];
    }
    else
    {
        title = [title stringByAppendingString:@"Redeem Failed"];
        message = [message stringByAppendingString:@"Your gift has not been redeemed. Please try again later."];
    }
    
    UIAlertController* alert = [UIAlertController alertControllerWithTitle:title message:message preferredStyle:UIAlertControllerStyleAlert];
    UIAlertAction* acknowledgeAction = [UIAlertAction actionWithTitle:@"Ok" style:UIAlertActionStyleDefault handler:nil];
    [alert addAction:acknowledgeAction];
    [self presentViewController:alert animated:YES completion:nil];
}

@end
