# Frontend

**Điều kiện cần để chạy frontend:** Máy có cài NodeJs, nếu chưa có thì xem bên dưới.

**Cách chạy frontend:**

- Khi mới clone repo về máy, cần cài các packages mà frontend dùng:
```bash
cd frontend
npm install
```

- Mỗi lần chạy frontend:
```bash
cd frontend
npm run dev
```

### How to install NodeJs

1. Download: https://nodejs.org/en/download

2. Install the windows .msi file
3. Run the file: → Next → Next → Finish

4. Check:
```bash
node -v
npm -v
```

## Project Architecture

```
└── 📁src
    └── 📁api
        ├── adminApi.js
        ├── authApi.js
        ├── axiosClient.js
        ├── fileApi.js
        ├── lecturerApi.js
        ├── newsApi.js
        ├── studentApi.js
    └── 📁assets
    └── 📁components
        └── 📁Layout
            ├── AdminLayout.jsx
            ├── UserLayout.jsx
        └── 📁modal
            ├── ApprovalModal.jsx
            ├── ConfirmModal.jsx
            ├── FormModal.jsx
            ├── Modal.jsx
        └── 📁parts
            ├── Button.jsx
            ├── DonutChart.jsx
            ├── FilterForm.jsx
            ├── IconButton.jsx
            ├── PopupMessage.jsx
            ├── SubjectMultiSelect.jsx
            ├── SubjectSelect.jsx
        └── 📁table
            ├── DataTable.jsx
            ├── FileCard.jsx
            ├── Pagination.jsx
    └── 📁constants
        ├── admin.js
        ├── file.js
        ├── layout.js
        ├── lecturer.js
        ├── news.js
    └── 📁helpers
        ├── adminHelpers.js
        ├── fileHelpers.js
        ├── commonHelpers.js
        ├── lecturerHelpers.js
        ├── newsHelpers.js
        ├── studentHelpers.js
    └── 📁pages
        └── 📁Admin
            └── 📁file
                ├── FileList.jsx
                ├── FileRequests.jsx
                ├── ManageFiles.jsx
            └── 📁manages
                ├── ManageAdmins.jsx
                ├── ManageLecturers.jsx
                ├── ManageNews.jsx
                ├── ManageReports.jsx
            └── 📁news
                ├── NewsList.jsx
                ├── NewsRequests.jsx
                ├── ManageNewss.jsx
        └── 📁Auth
            ├── LoginPage.jsx
        └── 📁File
            ├── FileDetail.jsx
            ├── FileList.jsx
        └── 📁Home
            ├── HomePage.jsx
        └── 📁Lecturer
            ├── LecturerDetail.jsx
            ├── LecturerList.jsx
        └── 📁News
            ├── NewsDetail.jsx
            ├── NewsList.jsx
        └── 📁Student
            ├── StudentPortal.jsx
    ├── App.jsx
    ├── index.css
    └── main.jsx
```



