using Microsoft.EntityFrameworkCore;
using ShoesStore.Areas.Admin.InterfaceRepositories;
using ShoesStore.Areas.Admin.Repositories;
using ShoesStore.InterfaceRepositories;
using ShoesStore.Models;
using ShoesStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ================= SERVICES =================

builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); // ?? dùng API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // ?? b?t swagger

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddDbContext<ShoesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Shoes"));
});

builder.Services.AddTransient<IEmailSender, EmailSender>();

// ==== Scoped Repositories ====
builder.Services.AddScoped<IPhieuMuaAdmin, PhieuMuaAdminRepo>();
builder.Services.AddScoped<IDongsanphamAdmin, DongsanphamAdminRepo>();
builder.Services.AddScoped<IKhuyenMaiAdmin, KhuyenMaiAdminRepo>();
builder.Services.AddScoped<INhanvien, NhanvienAdminRepo>();
builder.Services.AddScoped<IBlogAdmin, BlogAdminRepo>();
builder.Services.AddScoped<ILoaiAdmin, LoaiAdminRepo>();
builder.Services.AddScoped<IMauAdmin, MauAdminRepo>();
builder.Services.AddScoped<ISizeAdmin, SizeAdminRepo>();
builder.Services.AddScoped<ISpSizeAdmin, SpSizeAdminRepo>();
builder.Services.AddScoped<ISanPhamAdmin, SanPhamAdminRepo>();
builder.Services.AddScoped<IDongSanpham, DongSanphamRepo>();
builder.Services.AddScoped<IPhuongthucthanhtoan, PhuongthucthanhtoanRepo>();
builder.Services.AddScoped<ISanpham, SanphamRepo>();
builder.Services.AddScoped<ISize, SizeRepo>();
builder.Services.AddScoped<ISanphamSize, SanphamSizeRepo>();
builder.Services.AddScoped<IMau, MauRepo>();
builder.Services.AddScoped<IKhachhang, KhachhangRepo>();
builder.Services.AddScoped<IPhieuMua, PhieuMuaRepo>();
builder.Services.AddScoped<IKhuyenMai, KhuyenMaiRepo>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<IBinhLuan, BinhLuanRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IAddressNoteBook, AddressNoteBookRepo>();
builder.Services.AddScoped<IVoucher, VoucherRepo>();
builder.Services.AddScoped<IVoucherAdmin, VoucherAdminRepo>();

var app = builder.Build();

// ================= MIDDLEWARE =================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // ?? b?t swagger
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

// ===== Area Route =====
app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=HomeAdmin}/{action=Index}/{id?}");

// ===== Default MVC =====
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ===== API =====
app.MapControllers();

app.Run();