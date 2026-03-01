# 🚀 Universal Base64 Decoder

A live web utility that decodes Base64 strings and intelligently previews the output with multi-format export support.

🌐 **Live Website:** https://universal-base64-decoder.onrender.com/

---

## ✨ Features

- 🔍 Smart file detection (PNG, JPEG, PDF, ZPL)
- 👀 Inline previews directly in browser
- 📦 Multi-format export support
- 🏷️ ZPL label rendering (PNG + PDF conversion)
- ⚡ No uploads — fully client-driven decoding
- 🌍 Live deployment

---

## 🧠 Supported Formats

| Format | Preview | Export |
|--------|--------|--------|
| PNG / JPEG | ✅ Inline image | Original |
| PDF | ✅ Embedded viewer | Original |
| ZPL | ✅ Rendered label | PNG + PDF |
| Unknown Binary | Text preview | Raw file |

---

## 🛠 Tech Stack

- **Backend:** ASP.NET Core (Razor Pages)
- **Language:** C#
- **PDF Engine:** QuestPDF
- **ZPL Rendering:** Labelary API
- **Deployment:** Render (free tier)
- **Version Control:** Git + GitHub

---

## 🏗 Architecture
Core
├── Interfaces
└── Models

Services
├── Decoding
├── Detection
├── Rendering
└── Export

Web
└── Razor Pages UI


---

## 🎯 Use Cases

- Debugging Base64 APIs
- Viewing shipping labels (ZPL)
- Inspecting encoded files
- Developer tooling

---

## 📦 Getting Started (Local Setup)
```bash
git clone https://github.com/kanishka0909/universal-base64-decoder.git
cd universal-base64-decoder/src/UniversalBase64Decoder.Web
dotnet run

Open in browser:

http://localhost:5000

📜 License

MIT License — free to use and modify.

👨‍💻 Author

Kanishka
Computer Science Engineer • Software Developer
