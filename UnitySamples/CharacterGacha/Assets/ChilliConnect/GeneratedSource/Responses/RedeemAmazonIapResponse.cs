//
//  This file was auto-generated using the ChilliConnect SDK Generator.
//
//  The MIT License (MIT)
//
//  Copyright (c) 2015 Tag Games Ltd
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using SdkCore;

namespace ChilliConnect 
{
	/// <summary>
	/// A container for information on the response from a RedeemAmazonIapRequest.
	/// </summary>
	public sealed class RedeemAmazonIapResponse
	{
		/// <summary>
		/// True if the receipt data was successfully verified and the rewards were applied
		/// to the players account. Otherwise, false. In the case of false, the Status field
		/// contains additional information on reason the receipt could not be redeemed.
		/// </summary>
        public bool Redeemed { get; private set; }
	
		/// <summary>
		/// Detailed status for the receipt. This can be one of: Valid: The purchase was
		/// valid; ValidNotRedeemed: The purchase was valid but seen before, but had not yet
		/// been redeemed. InvalidRedeemed: The purchase has already been redeemed.
		/// InvalidVerificationFailed: The Amazon Receipt Verification Service returned that
		/// the provided receipt data was not valid; InvalidVerifiedForAnotherPlayer: The
		/// Receipt has previously been sent to ChilliConnect by a different player and
		/// validated.
		/// </summary>
        public string Status { get; private set; }
	
		/// <summary>
		/// Contains detailed information about the response from the Amazon Receipt
		/// Verification Service.
		/// </summary>
        public ReceiptVerificationService ReceiptVerificationService { get; private set; }
	
		/// <summary>
		/// The rewards that were applied to the player's account.
		/// </summary>
        public PurchaseExchange Rewards { get; private set; }

		/// <summary>
		/// Initialises the response with the given json dictionary.
		/// </summary>
		///
		/// <param name="jsonDictionary">The dictionary containing the JSON data.</param>
		public RedeemAmazonIapResponse(IDictionary<string, object> jsonDictionary)
		{
			ReleaseAssert.IsNotNull(jsonDictionary, "JSON dictionary cannot be null.");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Redeemed"), "Json is missing required field 'Redeemed'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("Status"), "Json is missing required field 'Status'");
			ReleaseAssert.IsTrue(jsonDictionary.ContainsKey("ReceiptVerificationService"), "Json is missing required field 'ReceiptVerificationService'");
	
			foreach (KeyValuePair<string, object> entry in jsonDictionary)
			{
				// Redeemed
				if (entry.Key == "Redeemed")
				{
                    ReleaseAssert.IsTrue(entry.Value is bool, "Invalid serialised type.");
                    Redeemed = (bool)entry.Value;
				}
		
				// Status
				else if (entry.Key == "Status")
				{
                    ReleaseAssert.IsTrue(entry.Value is string, "Invalid serialised type.");
                    Status = (string)entry.Value;
				}
		
				// Receipt Verification Service
				else if (entry.Key == "ReceiptVerificationService")
				{
                    ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                    ReceiptVerificationService = new ReceiptVerificationService((IDictionary<string, object>)entry.Value);	
				}
		
				// Rewards
				else if (entry.Key == "Rewards")
				{
					if (entry.Value != null)
					{
                        ReleaseAssert.IsTrue(entry.Value is IDictionary<string, object>, "Invalid serialised type.");
                        Rewards = new PurchaseExchange((IDictionary<string, object>)entry.Value);	
                    }
				}
			}
		}
	}
}
