global using JobSeekerSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// 加入 MVC 支持
builder.Services.AddControllersWithViews();

// 数据库连接（LocalDB）
builder.Services.AddSqlServer<DB>($@"
    Data Source=(LocalDB)\MSSQLLocalDB;
    AttachDbFilename={builder.Environment.ContentRootPath}\DB.mdf;
");

var app = builder.Build();

// 异常页面（生产模式使用）
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTP Strict Transport Security
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 使用默认 MVC 路由：/{controller=Home}/{action=Index}/{id?}
app.MapDefaultControllerRoute();

app.Run();

// Testing2
// Testing4
// Testing5
// Testing6