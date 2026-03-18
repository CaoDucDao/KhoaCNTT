export const columns = [
  {
    key: "fullName",
    label: "Tên giảng viên",
  },

  {
    key: "degree",
    label: "Học vị",
  },

  {
    key: "position",
    label: "Chức vụ",
  },

  {
    key: "email",
    label: "Email",
  },

  {
    key: "phoneNumber",
    label: "Điện thoại",
  },
];

export const fields = [
  {
    name: "fullName",
    label: "Họ tên",
    required: true,
  },

  {
    name: "imageUrl",
    label: "Ảnh",
  },

  {
    name: "degree",
    label: "Học vị",
    type: "select",
    required: true,
    options: [
      { label: "Bachelor", value: "Bachelor" },
      { label: "Master", value: "Master" },
      { label: "Doctor", value: "Doctor" },
      { label: "Professor", value: "Professor" },
      { label: "Associate Professor", value: "Associate Professor" },
    ],
  },

  {
    name: "position",
    label: "Chức vụ",
    required: true,
    type: "select",
    options: [
      { label: "Giảng viên", value: "Giảng viên" },
      { label: "Trưởng bộ môn", value: "Trưởng bộ môn" },
      { label: "Phó trưởng bộ môn", value: "Phó trưởng bộ môn" },
      { label: "Trưởng khoa", value: "Trưởng khoa" },
      { label: "Phó trưởng khoa", value: "Phó trưởng khoa" },
    ],
  },

  {
    name: "birthdate",
    label: "Ngày sinh",
    type: "date",
    required: true,
  },

  {
    name: "email",
    label: "Email",
    type: "email",
    validate: (value) => {
      if (!value) return null; // không bắt buộc, bỏ qua nếu trống
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(value)) {
        return "Email không đúng định dạng (ví dụ: example@domain.com)";
      }
      return null; // hợp lệ
    },
  },

  {
    name: "phoneNumber",
    label: "Số điện thoại",
  },

  {
    name: "subjectCodes",
    label: "Môn giảng dạy",
    fullWidth: true,
  },
];
export const validateFields = (formData) => {
  const errors = {};

  for (const field of fields) {
    const value = formData[field.name];

    // Check required
    if (
      field.required &&
      (value === undefined || value === null || value === "")
    ) {
      errors[field.name] = `${field.label} là bắt buộc`;
      continue;
    }

    // Check custom validate (dùng cho email, v.v.)
    if (field.validate && value) {
      const msg = field.validate(value);
      if (msg) errors[field.name] = msg;
    }
  }

  return errors;
};
