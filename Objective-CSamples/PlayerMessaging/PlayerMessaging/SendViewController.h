//
//  SendViewController.h
//  PlayerMessaging
//
//  Created by Robert Henning on 11/11/2016.
//  Copyright © 2016 Chilli Technologies. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface SendViewController : UITableViewController

@property (nonatomic, strong) NSArray* friends;
@property (nonatomic) NSUInteger selectedRow;

@end
