using System;

namespace ComicsManager.BackOffice.ViewModels.Home
{
    public class SimpleComicViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public Guid? CouvertureFileId { get; set; }
    }
}
