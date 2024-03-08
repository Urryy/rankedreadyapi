using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankedReadyApi.Common.Context;

public class ApplicationDataBaseContext : DbContext
{
	public ApplicationDataBaseContext(DbContextOptions<ApplicationDataBaseContext> options) : base(options)
	{

	}
}
