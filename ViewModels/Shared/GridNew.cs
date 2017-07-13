using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSM.Common;

namespace SSM.ViewModels.Shared
{

	public class GridNew <T, E>  
	{
		private Pager _pager;
		private T _searchCriteria;
        private E _model;
		private IEnumerable<T> _data;

		private GridAction _gridAction;
		
        
		//Properties
		public Pager Pager
		{
			get { return _pager; }
			set { _pager = value; } 
		}

		public T SearchCriteria
		{
			get { return _searchCriteria; }
			set { _searchCriteria = value; }
		}

        public E Model
        {
            get { return _model; }
            set { _model = value; }
        }

		public IEnumerable<T> Data
		{ 
			get { return _data; }
			set { _data = value; } 
		}

		public GridAction GridAction
		{
			get { return _gridAction; }
			set { _gridAction = value; }
		}


		public GridNew()
		{
			_pager = new Pager();
		}

		public GridNew(Pager pager)
		{
            Verify.Argument.IsNotNull(pager, "pager");
			_pager = pager;
		}


		public void ProcessAction()
		{
			if (_gridAction == GridAction.ChangePageSize
				|| _gridAction == GridAction.Sort
				|| _gridAction == GridAction.Search	)
				_pager.CurrentPage = 1;

			_gridAction = GridAction.None;
		}


		public static SelectList PageSizeSelectList()
		{
			var pageSizes = new List<string> { "10", "20", "50", "100" };
			return new SelectList(pageSizes, "Value");
		}
		
	}
}