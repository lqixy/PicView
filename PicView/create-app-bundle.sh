#!/bin/bash

# 设置变量
APP_NAME="PicView"
PUBLISH_DIR="bin/Release/net9.0/osx-arm64/publish"
BUNDLE_DIR="bin/Release/net9.0/osx-arm64/$APP_NAME.app"
CONTENTS_DIR="$BUNDLE_DIR/Contents"
MACOS_DIR="$CONTENTS_DIR/MacOS"
RESOURCES_DIR="$CONTENTS_DIR/Resources"

# 创建应用程序包结构
mkdir -p "$MACOS_DIR"
mkdir -p "$RESOURCES_DIR"

# 复制可执行文件和库
cp "$PUBLISH_DIR/$APP_NAME" "$MACOS_DIR/"
cp "$PUBLISH_DIR/lib"*.dylib "$MACOS_DIR/"

# 复制资源
cp -r "Assets" "$RESOURCES_DIR/"

# 创建Info.plist文件
cat > "$CONTENTS_DIR/Info.plist" << EOF
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>CFBundleIconFile</key>
    <string>avalonia-logo.ico</string>
    <key>CFBundleIdentifier</key>
    <string>com.example.picview</string>
    <key>CFBundleName</key>
    <string>PicView</string>
    <key>CFBundleDisplayName</key>
    <string>PicView</string>
    <key>CFBundleExecutable</key>
    <string>PicView</string>
    <key>CFBundleVersion</key>
    <string>1.0.0</string>
    <key>CFBundleShortVersionString</key>
    <string>1.0.0</string>
    <key>CFBundleInfoDictionaryVersion</key>
    <string>6.0</string>
    <key>CFBundlePackageType</key>
    <string>APPL</string>
    <key>CFBundleSignature</key>
    <string>????</string>
    <key>NSHighResolutionCapable</key>
    <true/>
    <key>NSPrincipalClass</key>
    <string>NSApplication</string>
    <key>LSMinimumSystemVersion</key>
    <string>10.15</string>
</dict>
</plist>
EOF

echo "应用程序包已创建: $BUNDLE_DIR" 