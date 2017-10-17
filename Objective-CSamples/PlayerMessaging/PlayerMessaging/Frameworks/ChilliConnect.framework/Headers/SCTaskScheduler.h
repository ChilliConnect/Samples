//
//  Created by Ian Copland on 2015-08-27
//
//  The MIT License (MIT)
//
//  Copyright (c) 2015 Tag Games Limited
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//

@import Foundation;

NS_ASSUME_NONNULL_BEGIN

/*!
 A typdef for a simple block that takes no parameters and returns no value. The task
 scheduler will use this as a Task.
 */
typedef void (^SCTask)(void);

/*!
 Provides task based threading using blocks. There are two types of task: main thread
 and background thread. Main thread tasks are executed on applications main thread
 and are tyically used for callbacks from methods. Background tasks are performed
 on any background thread and should be used for most functionality, including
 file input and output, and networked calls.
 
 Both types of task can be scheduled from any thread.
 
 @author Ian Copland
 */
@interface SCTaskScheduler : NSObject {
@private
    NSOperationQueue *_backgroundQueue;
}

/*!
 A factory method for creating a new instance of the task scheduler.
 
 @return The new task scheduler instance.
 */
+ (instancetype)taskScheduler;

/*!
 Initialises the task scheduler.
 
 @return The initialised task scheduler instance.
 */
- (instancetype)init NS_DESIGNATED_INITIALIZER;

/*!
 Schedules a task which will be run on the applications main thread. This can be called from
 any thread.
 
 @param task The task which should be performed on the main thread.
 */
- (void)scheduleMainThreadTask:(SCTask)task;

/*!
 Schedules a task which will be run on the application's main thread. This can be called from
 any thread. The current thread will block until the task is completed.
 
 @param task The task which should be performed on the main thread.
 
 @throws InvalidOperationException This will be thrown if trying to wait for a main thread 
 task is finished from the main thread. This would result in a deadlock otherwise.
 */
- (void)scheduleMainThreadTaskAndWait:(SCTask)task;

/*!
 Schedules a task which will be run on a background thread. This can be called from any thread. 
 Tasks will not necessarily be executed immediately. If there are already a number of active 
 tasks they will be queued and executed when other tasks finish.
 
 @param task The task which should be performed on a background thread.
 */
- (void)scheduleBackgroundTask:(SCTask)task;

/*!
 Schedules a task which will be run on a background thread. This can be called from any thread. 
 Tasks will not necessarily be executed immediately. If there are already a number of active 
 tasks they will be queued and executed when other tasks finish.  The current thread will block
 until the task is completed.
 
 @param task The task which should be performed on a background thread.
 */
- (void)scheduleBackgroundTaskAndWait:(SCTask)task;

@end

NS_ASSUME_NONNULL_END