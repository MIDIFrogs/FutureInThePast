using MIDIFrogs.FutureInThePast.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.DialogSystem
{
	public class LevelDialogRunner : MonoBehaviour
	{
		[SerializeField] private DialogPlayer dialogPlayer;

		[Header("Level requirements")]
		[SerializeField] private List<ItemInfo> goodItems;
		[SerializeField] private PriceMaker priceMaker;

		[Header("Level start")]
		[SerializeField] private Dialog startDialog;

		[Header("Level trade dialogs")]
		[SerializeField] private Dialog sellGoodPriceDialog;
		[SerializeField] private Dialog sellBadPriceDialog;

		[Header("Level epilogue dialogs")]
		[SerializeField] private Dialog goodEndingDialog;
		[SerializeField] private Dialog badEndingDialog;

		public PriceMaker PriceMaker => priceMaker;

        public async void Start()
        {
			// Begin start dialog.
			await dialogPlayer.StartDialogAsync(startDialog);
        }

		public bool IsItemGood(ItemInfo item) => goodItems.Contains(item);

		public async void OnItemSold(ItemInfo item, int price)
		{
			bool itemGood = IsItemGood(item);
			bool priceGood = priceMaker.IsPriceGood(item, price);
			GameResult result;
			Dialog dialogToPlay;
			Dialog epilogue;
			if (priceGood)
			{
				dialogToPlay = sellGoodPriceDialog;
				if (itemGood)
				{
					result = GameResult.Good;
					epilogue = goodEndingDialog;
				}
				else
				{
					result = GameResult.Bad;
					epilogue = badEndingDialog;
				}
			}
			else
			{
				dialogToPlay = sellBadPriceDialog;
				if (itemGood)
				{
					result = GameResult.Neutral;
					epilogue = goodEndingDialog;
				}
				else
				{
					result = GameResult.Fail;
					epilogue = badEndingDialog;
				}
			}
			Debug.Log($"Level ended with result {result}. Starting playing dialogs.");
			await dialogPlayer.StartDialogAsync(dialogToPlay);
			await dialogPlayer.StartDialogAsync(epilogue);
			PlayerStats.Session.LevelCompleted(result, price);
		}
    }
}