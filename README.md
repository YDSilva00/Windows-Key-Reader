# WinKeyShower

A simple C# console application that retrieves your Windows product key from the local machine's registry.

## ⚠️ Disclaimer

This tool is intended **only for retrieving your own Windows product key** from your own machine.
Use it responsibly and in accordance with Microsoft's licensing terms.

---

## 📋 Requirements

- Windows OS
- .NET Framework 4.8
- Administrator privileges (required to read the registry)

---

## 🚀 How to Use

### Option 1: Quick PowerShell Check (OEM keys only)
If your PC came with Windows pre-installed, run this in PowerShell as Administrator:
```powershell
(Get-WmiObject -query 'select * from SoftwareLicensingService').OA3xOriginalProductKey
```

### Option 2: Build & Run the App

1. Clone the repository:
```bash
git clone https://github.com/YDSilva00/Windows-Key-Reader.git
cd Windows-Key-Reader
```

2. Open and Build in Visual Studio:
- Open `WinKeyShower.sln` using Visual Studio.
- Build the solution (Ctrl + Shift + B).

3. Run the App:
- Navigate to `WinKeyShower\bin\Debug` or `WinKeyShower\bin\Release` and run `WinKeyShower.exe`. Note that running as Administrator might be required to read the registry successfully.
- Alternatively, run the project directly from within Visual Studio.

---

## 🔧 How It Works

Windows stores the product key in the registry under:
`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion`

The key is stored as an encoded byte array (`DigitalProductId`).
This app decodes that byte array back into the standard `XXXXX-XXXXX-XXXXX-XXXXX-XXXXX` format, including support for Windows 8, 10, and 11 style keys.

---

## 📁 Project Structure
```text
WinKeyShower/
├── WinKeyShower.sln                # Visual Studio Solution
└── WinKeyShower/
    ├── Program.cs                  # Main source file containing the registry reading and decoding logic
    ├── WinKeyShower.csproj         # Project configuration file (.NET Framework 4.8)
    └── Properties/
        └── AssemblyInfo.cs         # Assembly information
```

## 🛡️ Privacy Note

This application **only reads from your local machine's registry**.
It does **not** send any data over the network.

---

## 📄 License

MIT License — free to use, modify, and distribute.
