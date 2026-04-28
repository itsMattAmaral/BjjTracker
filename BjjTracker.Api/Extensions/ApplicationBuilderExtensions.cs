namespace BjjTracker.Api.Extensions;

public static class ApplicationBuilderExtensions
{
	public static WebApplication ConfigurePipeline(this WebApplication app, IHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "BjjTracker API");
				options.RoutePrefix = string.Empty;
			});
		}
		if (!env.IsDevelopment())
		{
			app.UseHttpsRedirection();
		}
		
		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		return app;
	}
}