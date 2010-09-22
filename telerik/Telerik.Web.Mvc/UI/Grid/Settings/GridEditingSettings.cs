// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure;

    public class GridEditingSettings : IClientSerializable
    {
        private readonly IGrid grid;

        public GridEditingSettings(IGrid grid)
        {
            this.grid = grid;

            DisplayDeleteConfirmation = true;
        }

        public Window PopUp
        {
            get;
            set;
        }

        public GridEditMode Mode
        {
            get;
            set;
        }

        public bool Enabled 
        { 
            get; 
            set; 
        }

        public bool DisplayDeleteConfirmation
        {
            get;
            set;
        }

        public IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            FluentDictionary.For(result)
                .Add("confirmDelete", DisplayDeleteConfirmation, true)
                .Add("mode", Mode.ToString())
#if MVC2
                .Add("editor", grid.EditorHtml, () => Mode != GridEditMode.InLine)
#endif
                .Add("popup", SerializePopUp(), () => Mode == GridEditMode.PopUp && grid.IsClientBinding);

            return result;
        }

        private IDictionary<string, object> SerializePopUp()
        {
            var result = new Dictionary<string, object>();
            FluentDictionary.For(result)
                .Add("title", PopUp.Title, "")
                .Add("modal", PopUp.Modal)
                .Add("draggable", PopUp.Modal)
                .Add("resizable", PopUp.ResizingSettings.Enabled);

            return result;
        }

        public void SerializeTo(string key, IClientSideObjectWriter writer)
        {
            if (!Enabled)
            {
                return;
            }

            var editing = Serialize();

            if (editing.Any())
            {
                writer.AppendObject("editing", editing);
            }

            if (grid.IsClientBinding)
            {
                writer.AppendObject("dataKeys", grid.DataKeys.ToDictionary(dataKey => dataKey.Name, dataKey => (object)dataKey.RouteKey));

                if (!grid.IsEmpty)
                {
                    if (grid.DataProcessor.ProcessedDataSource is IQueryable<AggregateFunctionsGroup>)
                    {
                        IEnumerable<IGroup> grouppedDataSource = grid.DataProcessor.ProcessedDataSource.Cast<IGroup>();
                        writer.AppendCollection("data", grouppedDataSource.Leaves());
                    }
                    else
                    {
                        writer.AppendCollection("data", grid.DataProcessor.ProcessedDataSource);
                    }
                }
            }
        }
    }
}
