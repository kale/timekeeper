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
public class CategoriesBLL
{
	private CategoriesTableAdapter _CategoriesAdaptor = null;

	protected CategoriesTableAdapter Adaptor
	{
		get
		{
			if (_CategoriesAdaptor == null)
				_CategoriesAdaptor = new CategoriesTableAdapter();

			return _CategoriesAdaptor;
		}
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.CategoriesDataTable GetCategories()
	{
		return Adaptor.GetCategories();
	}

	/*
	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.CategoriesDataTable GetCategoryByCategoryID(int categoryID)
	{
		return Adaptor.GetCategoryByCategoryID(categoryID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
	public bool AddCategory(int userID, DateTime dateTime, string categoryText)
	{
		//Create a new CategoryRow instance
		TimeKeeper.CategoriesDataTable Categories = new TimeKeeper.CategoriesDataTable();
		TimeKeeper.CategoriesRow category = Categories.NewCategoriesRow();

		category.UserId = userID;
		category.Date = dateTime;
		category.CategoryText = categoryText;

		//Add the new category
		Categories.AddCategoriesRow(category);
		int rowsAffected = Adaptor.Update(Categories);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
	public bool UpdateCategory(int userID, DateTime dateTime, string categoryText, int categoryID)
	{
		TimeKeeper.CategoriesDataTable Categories = Adaptor.GetCategoryByCategoryID(categoryID);
		if (Categories.Count == 0)
			return false;

		TimeKeeper.CategoriesRow category = Categories[0];

		category.UserId = userID;
		category.Date = dateTime;
		category.CategoryText = categoryText;

		//Add the new category
		int rowsAffected = Adaptor.Update(category);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
	public bool DeleteCategory(int categoryID)
	{
		int rowsAffected = Adaptor.Delete(categoryID);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}
	*/
}