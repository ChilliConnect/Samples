//
//  Created by Nicolas Tanda on 2017-09-18
//
//  The MIT License (MIT)
//
//  Copyright (c) 2017 Tag Games Limited
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Diagnostics;

namespace SdkCore
{
	/// <summary>
	/// <para>Provides the means to schedule tasks on a background thread or the 
	/// main thread. Also allows for creation of co-routines from classes that do
	/// not inherit from MonoBehaviour.</para>
	/// 
	/// <para>This is thread safe, through it must be constructed on the main thread.
	/// </para>
	/// </summary>
	public sealed class TaskSchedulerWebGL : TaskScheduler
	{
		private Queue<Action> m_mainThreadTaskQueue = new Queue<Action>();

		/// <summary>
		/// Schedules a new task on a background thread.
		/// NOTE: In WebGL, multi-threading isn't supported, so this will be scheduled on main thread instead.
		/// </summary>
		/// 
		/// <param name="task">The task that should be executed on a background thread.</param>
		public override void ScheduleBackgroundTask(Action task)
		{
			ScheduleMainThreadTask(task);
		}

		/// <summary>
		/// Schedules a new task on the main thread. The task will be executed during the
		/// next update.
		/// </summary>
		/// 
		/// <param name="task">The task that should be executed on the main thread.</param>
		public override void ScheduleMainThreadTask(Action task)
		{
			m_mainThreadTaskQueue.Enqueue(task);
		}

		/// <summary>
		/// The update method which is called every frame. This executes any queued main
		/// thread tasks.
		/// </summary>
		void Update()
		{
			Queue<Action> taskQueue = new Queue<Action>(m_mainThreadTaskQueue);
			m_mainThreadTaskQueue.Clear();

			foreach(Action action in taskQueue)
			{
				action();
			}
		}
	}
}