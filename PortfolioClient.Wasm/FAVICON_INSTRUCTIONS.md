# Favicon Generation Instructions

Since we can't generate actual image files programmatically, you'll need to create favicon images using one of these tools:

## Option 1: Use Favicon Generator (Recommended)

1. Visit **https://realfavicongenerator.net/**
2. Upload your logo or create a simple design with your initials "DM"
3. Configure settings:
   - **iOS**: Generate apple-touch-icon (180x180)
   - **Android Chrome**: Generate icon-192.png and icon-512.png
   - **Classic**: Generate favicon-16x16.png and favicon-32x32.png
4. Download the generated package
5. Copy the files to `wwwroot/` folder

## Option 2: Use Favicon.io

1. Visit **https://favicon.io/**
2. Choose "Text" to create initials-based favicon with "DM"
3. Select:
   - Font: Choose a modern font
   - Background: #667eea (purple gradient color)
   - Text Color: #ffffff (white)
4. Download and extract
5. Copy files to `wwwroot/` folder

## Option 3: Design Your Own

1. Use design tool (Figma, Canva, Photoshop, etc.)
2. Create designs with "DM" or your logo:
   - **favicon-16x16.png** (16x16 pixels)
   - **favicon-32x32.png** (32x32 pixels)
   - **favicon.png** (192x192 pixels, fallback)
   - **icon-192.png** (192x192 pixels, for PWA)
   - **icon-512.png** (512x512 pixels, for PWA)
   - **apple-touch-icon.png** (180x180 pixels, for iOS)
3. Export as PNG with transparent background
4. Place in `wwwroot/` folder

## Required Files

After generation, your `wwwroot/` folder should contain:

- ✅ favicon-16x16.png
- ✅ favicon-32x32.png
- ✅ favicon.png (192x192)
- ✅ icon-192.png
- ✅ icon-512.png
- ✅ apple-touch-icon.png
- ✅ manifest.json (already created)

## Design Recommendations

- Use your initials "DM" prominently
- Gradient background (#667eea to #764ba2) matching your site theme
- White text for good contrast
- Simple, clean design that's recognizable at small sizes
- Square format with rounded corners for modern look

## Verification

After adding files:

1. Check browser tab shows new favicon
2. Test PWA installation on mobile
3. Verify icons appear correctly in app drawer (Android) or home screen (iOS)
4. Run Lighthouse audit to verify PWA readiness
