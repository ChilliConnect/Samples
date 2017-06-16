//
//  InboxViewController.h
//  PlayerMessaging
//
//  Created by Robert Henning on 11/11/2016.
//  Copyright © 2016 Chilli Technologies. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface InboxViewController : UITableViewController

@property (nonatomic) NSArray* friends;
@property (nonatomic) NSArray* messages;
@property (nonatomic) NSUInteger selectedRow;

@end
