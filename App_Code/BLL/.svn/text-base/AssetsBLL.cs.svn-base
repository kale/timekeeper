using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TimeKeeperTableAdapters;

[System.ComponentModel.DataObject]
public class AssetsBLL
{
	private AssetsTableAdapter _AssetsAdaptor = null;

	protected AssetsTableAdapter Adaptor
	{
		get
		{
			if (_AssetsAdaptor == null)
				_AssetsAdaptor = new AssetsTableAdapter();

			return _AssetsAdaptor;
		}
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.AssetsDataTable GetAssets()
	{
		return Adaptor.GetAssets();
	}

	/*
	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.AssetsDataTable GetAssetByAssetID(int assetID)
	{
		return Adaptor.GetAssetByAssetID(assetID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
	public bool AddAsset(int userID, DateTime dateTime, string assetText)
	{
		//Create a new AssetRow instance
		TimeKeeper.AssetsDataTable Assets = new TimeKeeper.AssetsDataTable();
		TimeKeeper.AssetsRow asset = Assets.NewAssetsRow();

		asset.UserId = userID;
		asset.Date = dateTime;
		asset.AssetText = assetText;

		//Add the new asset
		Assets.AddAssetsRow(asset);
		int rowsAffected = Adaptor.Update(Assets);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
	public bool UpdateAsset(int userID, DateTime dateTime, string assetText, int assetID)
	{
		TimeKeeper.AssetsDataTable Assets = Adaptor.GetAssetByAssetID(assetID);
		if (Assets.Count == 0)
			return false;

		TimeKeeper.AssetsRow asset = Assets[0];

		asset.UserId = userID;
		asset.Date = dateTime;
		asset.AssetText = assetText;

		//Add the new asset
		int rowsAffected = Adaptor.Update(asset);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
	public bool DeleteAsset(int assetID)
	{
		int rowsAffected = Adaptor.Delete(assetID);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}
	*/
}