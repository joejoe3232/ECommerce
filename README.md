# 電子商務系統 (ECommerceApp)

這是一個使用 ASP.NET Core MVC 建立的電子商務管理系統，提供完整的產品和訂單管理功能。

## 功能特色

### 🛍️ 產品管理
- 產品的新增、編輯、刪除
- 產品分類管理
- 產品搜尋和篩選
- 庫存管理
- 產品圖片支援

### 📦 訂單管理
- 訂單建立和追蹤
- 訂單狀態管理（待處理、處理中、已出貨、已送達、已取消）
- 客戶資訊管理
- 訂單項目管理

### 🎨 使用者介面
- 響應式設計，支援各種裝置
- 現代化的 Bootstrap 5 介面
- Font Awesome 圖示
- 直觀的操作流程

## 技術架構

- **後端框架**: ASP.NET Core 8.0 MVC
- **資料庫**: SQL Server (LocalDB)
- **ORM**: Entity Framework Core
- **前端框架**: Bootstrap 5
- **圖示**: Font Awesome 5
- **開發語言**: C#

## 系統需求

- .NET 8.0 SDK
- SQL Server LocalDB 或 SQL Server Express
- Visual Studio 2022 或 Visual Studio Code

## 安裝和設定

### 1. 複製專案
```bash
git clone [repository-url]
cd ECommerce
```

### 2. 進入專案目錄
```bash
cd ECommerceApp
```

### 3. 還原套件
```bash
dotnet restore
```

### 4. 建立資料庫
```bash
dotnet ef database update
```

### 5. 執行專案
```bash
dotnet run
```

### 6. 開啟瀏覽器
前往 `https://localhost:5001` 或 `http://localhost:5000`

## 專案結構

```
ECommerce/
├── ECommerceApp/           # 主要專案目錄
│   ├── Controllers/        # 控制器
│   │   ├── HomeController.cs
│   │   ├── ProductsController.cs
│   │   └── OrdersController.cs
│   ├── Models/             # 資料模型
│   │   ├── Product.cs
│   │   ├── Category.cs
│   │   ├── Order.cs
│   │   └── OrderItem.cs
│   ├── Views/              # 視圖
│   │   ├── Home/           # 首頁相關視圖
│   │   ├── Products/       # 產品相關視圖
│   │   ├── Orders/         # 訂單相關視圖
│   │   └── Shared/         # 共用視圖
│   ├── Data/               # 資料存取層
│   │   └── ApplicationDbContext.cs
│   ├── wwwroot/            # 靜態檔案
│   ├── Program.cs          # 應用程式入口點
│   └── appsettings.json   # 設定檔
└── README.md               # 專案說明文件
```

## 資料庫模型

### Product (產品)
- Id: 產品編號
- Name: 產品名稱
- Description: 產品描述
- Price: 價格
- StockQuantity: 庫存數量
- ImageUrl: 圖片網址
- CategoryId: 分類編號
- CreatedAt: 建立時間
- UpdatedAt: 更新時間

### Category (分類)
- Id: 分類編號
- Name: 分類名稱
- Description: 分類描述
- ImageUrl: 分類圖片
- IsActive: 是否啟用
- CreatedAt: 建立時間

### Order (訂單)
- Id: 訂單編號
- OrderNumber: 訂單號碼
- CustomerName: 客戶姓名
- CustomerEmail: 客戶信箱
- CustomerPhone: 客戶電話
- ShippingAddress: 送貨地址
- Notes: 備註
- TotalAmount: 總金額
- Status: 訂單狀態
- OrderDate: 訂單日期

### OrderItem (訂單項目)
- Id: 項目編號
- OrderId: 訂單編號
- ProductId: 產品編號
- Quantity: 數量
- UnitPrice: 單價

## 使用說明

### 產品管理
1. 點擊導航選單的「產品管理」
2. 使用搜尋和分類篩選功能找到產品
3. 點擊「新增產品」來建立新產品
4. 使用編輯和刪除按鈕管理現有產品

### 訂單管理
1. 點擊導航選單的「訂單管理」
2. 查看所有訂單的狀態和資訊
3. 點擊「新增訂單」來建立新訂單
4. 使用狀態更新功能追蹤訂單進度

## 開發指南

### 新增功能
1. 在 `ECommerceApp/Models/` 資料夾建立新的模型類別
2. 在 `ECommerceApp/Controllers/` 資料夾建立對應的控制器
3. 在 `ECommerceApp/Views/` 資料夾建立相關的視圖
4. 更新 `ECommerceApp/Data/ApplicationDbContext.cs` 加入新的 DbSet

### 資料庫變更
```bash
# 進入專案目錄
cd ECommerceApp

# 建立新的遷移
dotnet ef migrations add InitialCreate

# 更新資料庫
dotnet ef database update

# 查看所有遷移
dotnet ef migrations list

# 查看資料庫狀態
dotnet ef database update --verbose

# 刪除現有資料庫
dotnet ef database drop

# 重新建立資料庫
dotnet ef database update


##如果專案還沒有 Migrations 目錄。建立初始的資料庫遷移：
步驟1：安裝 Entity Framework 工具（如果還沒安裝）
dotnet tool install --global dotnet-ef

步驟2：建立初始 Migration
dotnet ef migrations add InitialCreate

步驟3：更新資料庫
dotnet ef database update


## 授權

此專案採用 MIT 授權條款。

## 貢獻

歡迎提交 Issue 和 Pull Request 來改善這個專案。

## 聯絡資訊

如有任何問題或建議，請透過 GitHub Issues 與我們聯絡。
