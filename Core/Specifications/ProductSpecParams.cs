namespace Core.Specifications
{
	public class ProductSpecParams
	{
		private const int MaxPageSize = 50;
		public int PageIndex { get; set; } = 1;
		private int pageSize = 6;
		public int PageSize
		{
			get => this.pageSize;
			set => this.pageSize = value > MaxPageSize ? MaxPageSize : value;
		}

		public int? BrandId { get; set; }
		public int? TypeId { get; set; }
		public string Sort { get; set; }
		public string search;
		public string Search
		{
			get => this.search;
			set => this.search = value?.ToLower();
		}
	}
}