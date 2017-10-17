//
//  InboxViewCell.h
//  PlayerMessaging
//
//  Created by Robert Henning on 19/05/2017.
//  Copyright © 2017 Chilli Technologies. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface InboxViewCell : UITableViewCell

@property (nonatomic, strong) IBOutlet UIImageView* imageView;
@property (nonatomic, strong) IBOutlet UILabel* message;

@end
