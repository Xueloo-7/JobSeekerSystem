global using JobSeekerSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// ���� MVC ֧��
builder.Services.AddControllersWithViews();

// ���ݿ����ӣ�LocalDB��
builder.Services.AddSqlServer<DB>($@"
    Data Source=(LocalDB)\MSSQLLocalDB;
    AttachDbFilename={builder.Environment.ContentRootPath}\DB.mdf;
");

var app = builder.Build();

// �쳣ҳ�棨����ģʽʹ�ã�
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HTTP Strict Transport Security
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ʹ��Ĭ�� MVC ·�ɣ�/{controller=Home}/{action=Index}/{id?}
app.MapDefaultControllerRoute();

app.Run();

// Testing2
// Testing4
// Testing5
// Testing6