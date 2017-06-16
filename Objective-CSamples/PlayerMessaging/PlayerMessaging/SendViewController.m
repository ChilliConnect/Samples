//
//  SendViewController.m
//  PlayerMessaging
//
//  Created by Robert Henning on 11/11/2016.
//  Copyright © 2016 Chilli Technologies. All rights reserved.
//

#import "SendViewController.h"

#import "AppDelegate.h"
#import "ChilliConnectController.h"
#import "SendViewCell.h"

@implementation SendViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
    NSLog(@"SendViewController");
    
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
    NSUInteger value = [self.friends count];
    return value;
}

- (UITableViewCell*)tableView:(UITableView*)tableView cellForRowAtIndexPath:(NSIndexPath*)indexPath
{
    static NSString* cellIdentifier = @"sendTableCell";
    
    SendViewCell* cell = [tableView dequeueReusableCellWithIdentifier:cellIdentifier];
    if(cell == nil)
    {
        cell = [[SendViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:cellIdentifier];
    }
    
    CCFacebookPlayerWithProfileImage* friend = [self.friends objectAtIndex:[indexPath row]];
    
    [cell.displayName setText:friend.facebookName];
    
    NSData* imageData = [[NSData alloc] initWithContentsOfURL:[NSURL URLWithString:friend.facebookProfileImage]];
    [cell.imageView setImage:[UIImage imageWithData:imageData]];
    
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
        self.selectedRow = indexPath.row;
        CCFacebookPlayerWithProfileImage* friend = [self.friends objectAtIndex:[indexPath row]];
        [self showConfirmSendGiftAlert:friend.chilliConnectId withValue:10];
    }
}

- (void) refresh
{
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    ChilliConnectController* chilliConnectController = [app getChilliConnectController];
    NSAssert(chilliConnectController, @"Chilli Connect controller does not exist.");
    
    if([chilliConnectController hasFacebookFriends])
    {
        [self refreshFacebookFriendsTable:chilliConnectController.getCachedFacebookFriends];
    }
    else
    {
        ChilliConnectControllerCallback fetchFacebookFriendsCallback = ^(BOOL success, NSString* errorDescription)
        {
            if(success)
            {
                [self refreshFacebookFriendsTable:chilliConnectController.getCachedFacebookFriends];
            }
            else
            {
                NSLog(@"%@",errorDescription);
            }
        };
        [chilliConnectController fetchFacebookFriends:fetchFacebookFriendsCallback];
    }
}

- (void) refreshFacebookFriendsTable:(NSArray*)friends
{
    self.friends = friends;
    [self.tableView reloadData];
    [self.refreshControl endRefreshing];
}

- (void) showConfirmSendGiftAlert:(NSString*)recipientChilliConnectID withValue:(int)value
{
    if(recipientChilliConnectID == nil || value <= 0)
    {
        return;
    }
    
    NSString* message = [NSString stringWithFormat:@"Do you want to send %d coins?", value];
    UIAlertController* alert = [UIAlertController alertControllerWithTitle:@"Send Gift?" message:message preferredStyle:UIAlertControllerStyleAlert];
    
    UIAlertAction* sendAction = [UIAlertAction actionWithTitle:@"Send" style:UIAlertActionStyleDefault handler:^(UIAlertAction* action)
    {
        NSLog(@"Sending message to %@",recipientChilliConnectID);
        [self sendGift:recipientChilliConnectID value:10];
    }];
    
    UIAlertAction* dismissButton = [UIAlertAction actionWithTitle:@"Dismiss" style:UIAlertActionStyleDefault handler:nil];
    
    [alert addAction:dismissButton];
    [alert addAction:sendAction];
    [self presentViewController:alert animated:YES completion:nil];
}

- (void) sendGift:(NSString*)reciepientChilliConnectId value:(int)value
{
    AppDelegate *app = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    ChilliConnectController* chilliConnectController = [app getChilliConnectController];
    NSAssert(chilliConnectController, @"Chilli Connect controller does not exist.");
    
    // We need to use SCMultiTypeValue when sending messages using cloudcode.
    NSDictionary* dict = [NSDictionary dictionaryWithObjectsAndKeys:[SCMultiTypeValue multiTypeValueWithNumber:[NSNumber numberWithInt:value]],@"GiftValue",[SCMultiTypeValue multiTypeValueWithString:reciepientChilliConnectId],@"To",nil];
    ChilliConnectControllerCallback sendMessageWithScriptCallback = ^(BOOL success, NSString* errorDescription)
    {
        if(success)
        {
            NSLog(@"Sent successfully.");
        }
        else
        {
            NSLog(@"%@",errorDescription);
        }
        [self showSendGiftSuccessAlert:success];
    };
    [chilliConnectController sendMessageWithScriptKey:@"SENDMESSAGEGIFT" dictionary:dict callback:sendMessageWithScriptCallback];
}

- (void) showSendGiftSuccessAlert:(BOOL)success
{
    NSString* title = @"";
    NSString* message = @"";
    if(success)
    {
        title = [title stringByAppendingString:@"Gift Sent"];
        message = [message stringByAppendingString:@"Your gift was sent."];
    }
    else
    {
        title = [title stringByAppendingString:@"Send Failed"];
        message = [message stringByAppendingString:@"Your gift has not been sent. Please try again later."];
    }
    
    UIAlertController* alert = [UIAlertController alertControllerWithTitle:title message:message preferredStyle:UIAlertControllerStyleAlert];
    UIAlertAction* acknowledgeAction = [UIAlertAction actionWithTitle:@"Ok" style:UIAlertActionStyleDefault handler:nil];
    [alert addAction:acknowledgeAction];
    [self presentViewController:alert animated:YES completion:nil];
}

@end
