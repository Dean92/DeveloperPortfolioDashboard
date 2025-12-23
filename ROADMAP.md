# Portfolio Enhancement Roadmap

## 📋 Overview
This document outlines planned enhancements and new features for the Developer Portfolio Dashboard (Blazor WebAssembly).

**Last Updated:** December 23, 2025  
**Status:** Planning Phase

---

## 🎯 Planned Enhancements

### 1. Main Landing Page Updates

#### 1.1 Update Professional Expertise
**Priority:** High  
**Status:** Planned

**Current State:**
- Tagline rotates through: "digital experiences", "web applications", "user interfaces", "scalable solutions"
- Description mentions: C#, ASP.NET Core, Angular, Blazor, SQL Server

**Planned Changes:**
- **Add SaaS to expertise areas**
  - Update rotating tagline to include "SaaS applications"
  - Update description to highlight SaaS experience
  - Suggested text: "Specializing in C#, ASP.NET Core, Angular, Blazor, Microsoft SQL Server, and **SaaS application development**"

**Implementation Notes:**
```javascript
// Updated tagline rotation array
var texts = [
    'digital experiences', 
    'web applications', 
    'user interfaces', 
    'scalable solutions',
    'SaaS applications'  // NEW
];
```

**Files to Update:**
- `PortfolioClient.Wasm\Components\Pages\Home.razor` (line ~24-26)
- `PortfolioClient.Wasm\wwwroot\portfolio.js` (if external script is used)

---

### 2. Projects Section Enhancements

#### 2.1 Add Hockey Rink Project
**Priority:** High  
**Status:** ✅ **COMPLETED**

**Project Details:**

```csharp
new Project
{
    Id = 1,
    Title = "Hockey Rink",
    IntroDescription = "A Software as a Service (SaaS) solution for hockey rink management, streamlining operations for small businesses.",
    Description = "The frontend, built with Angular 20 and Bootstrap 5.3.3, provides an intuitive interface for users to register for sessions, process credit card payments, and manage their bookings. Features include session registration for hockey leagues, lessons, and figure skating programs with skill-level based league selection.",
    DetailedDescription = "The backend leverages .NET Core 9 API with Entity Framework Core and Azure SQL for robust data management. An administrative dashboard enables comprehensive management of leagues, sessions, teams, and payment processing. The application is deployed using GitHub Actions CI/CD pipeline to Azure Static Web Apps, ensuring reliable and scalable service delivery.",
    KeyFeatures = new List<string>
    {
        "<strong>User Registration & Payments:</strong> Seamless user registration with integrated credit card payment processing for all session types.",
        "<strong>Skill-Based League Management:</strong> Users can sign up for sessions matching their skill level, with automated team assignment and league organization.",
        "<strong>Administrative Dashboard:</strong> Comprehensive backend management system for leagues, sessions, teams, and payment tracking.",
        "<strong>Modern SaaS Architecture:</strong> Built with .NET Core 9, Angular 20, and Azure SQL, deployed via CI/CD pipeline to Azure Static Web Apps for enterprise-grade reliability."
    },
    Technologies = new List<string> { ".NET Core 9", "Angular 20", "Entity Framework Core", "Azure SQL", "Bootstrap 5.3.3", "GitHub Actions", "Azure Static Web Apps" },
    GitHubUrl = "https://github.com/Dean92/hockey-rink-website",
    ProjectUrl = "https://lively-river-0c3237510.1.azurestaticapps.net",
    StartDate = new DateTime(2025, 1, 1)
}
```

**Project Information:**
- ✅ **Purpose:** Software as a Service (SaaS) solution for hockey rink small business management
- ✅ **Technologies:** 
  - Backend: .NET Core 9 API, Entity Framework Core, Azure SQL
  - Frontend: Angular 20, Bootstrap 5.3.3 (CSS variables)
  - DevOps: GitHub Actions (CI/CD), Azure Static Web Apps
- ✅ **Key Features:**
  - User registration and credit card payment processing
  - Session registration with skill-level based league selection
  - Hockey team creation and league management
  - Administrative dashboard for managing leagues, sessions, and payments
- ✅ **GitHub URL:** https://github.com/Dean92/hockey-rink-website
- ✅ **Live Demo:** https://lively-river-0c3237510.1.azurestaticapps.net
- ✅ **Timeline:** 2025 - Development in progress

**Implementation Status:**
- ✅ Added to StaticProjectService (replaced Healthcare Customer Portal)
- ✅ Comprehensive project details documented
- ✅ Live demo URL included
- ✅ GitHub repository linked

**File Updated:**
- `PortfolioClient.Wasm\Services\StaticProjectService.cs` (Hockey Rink is now Project ID #1)

---

#### 2.2 Add Project Screenshots
**Priority:** High  
**Status:** Planned

**Current State:**
- Projects display as text-only cards
- No visual representation of projects

**Planned Changes:**
- Add image carousel or thumbnail gallery to the right side of each project card
- Support multiple screenshots per project (2-4 recommended)
- Responsive design: images stack below on mobile

**Design Mockup:**
```
┌─────────────────────────────────────────────────────┐
│  Project Card                                       │
├─────────────────────────┬───────────────────────────┤
│ Project Title           │   ┌───────────────────┐   │
│                         │   │                   │   │
│ Intro Description       │   │   Screenshot 1    │   │
│                         │   │                   │   │
│ Detailed Description    │   └───────────────────┘   │
│                         │   ┌───────────────────┐   │
│ Key Features:           │   │                   │   │
│ • Feature 1             │   │   Screenshot 2    │   │
│ • Feature 2             │   │                   │   │
│ • Feature 3             │   └───────────────────┘   │
│                         │                           │
│ [Tech] [Tags]           │                           │
│ [View Project] [GitHub] │                           │
└─────────────────────────┴───────────────────────────┘
```

**Technical Implementation:**

1. **Update Project Model:**
```csharp
// Add to PortfolioShared\Models\Project.cs
public List<string> Screenshots { get; set; } = new List<string>(); // Image URLs or paths
```

2. **Update ProjectsSection.razor:**
```razor
<article class="project-item" role="listitem">
    <div class="project-content-wrapper">
        <div class="project-content-text">
            <!-- Existing content (title, descriptions, features, etc.) -->
        </div>
        
        @if (project.Screenshots != null && project.Screenshots.Any())
        {
            <div class="project-screenshots">
                @foreach (var screenshot in project.Screenshots)
                {
                    <img 
                        src="@screenshot" 
                        alt="Screenshot of @project.Title" 
                        class="project-screenshot-img"
                        loading="lazy" />
                }
            </div>
        }
    </div>
</article>
```

3. **Add CSS Styling:**
```css
/* Add to app.css or project-specific CSS file */
.project-content-wrapper {
    display: grid;
    grid-template-columns: 1fr 300px; /* Adjust based on design */
    gap: 2rem;
}

.project-screenshots {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

.project-screenshot-img {
    width: 100%;
    height: auto;
    border-radius: 8px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease;
}

.project-screenshot-img:hover {
    transform: scale(1.05);
}

/* Mobile responsive */
@media (max-width: 768px) {
    .project-content-wrapper {
        grid-template-columns: 1fr;
    }
    
    .project-screenshots {
        flex-direction: row;
        overflow-x: auto;
    }
    
    .project-screenshot-img {
        min-width: 200px;
        max-width: 300px;
    }
}
```

4. **Image Storage:**
   - Store images in: `PortfolioClient.Wasm\wwwroot\images\projects\`
   - Organize by project: `images/projects/healthcare-portal/screenshot1.png`
   - Recommended formats: PNG or WebP
   - Recommended size: Max 800px width, optimized for web

5. **Update StaticProjectService:**
```csharp
new Project
{
    Id = 1,
    Title = "Healthcare Customer Portal",
    // ... existing properties ...
    Screenshots = new List<string>
    {
        "images/projects/healthcare-portal/dashboard.png",
        "images/projects/healthcare-portal/analytics.png"
    }
}
```

**Tasks for Implementation:**
- [ ] Modify Project.cs to add Screenshots property
- [ ] Create image directory structure
- [ ] Capture and optimize screenshots for each project
- [ ] Update StaticProjectService with image paths
- [ ] Update ProjectsSection.razor layout
- [ ] Add responsive CSS styling
- [ ] Test on mobile and desktop viewports
- [ ] Ensure accessibility (alt text, keyboard navigation)

**Files to Update:**
- `PortfolioShared\Models\Project.cs`
- `PortfolioClient.Wasm\Services\StaticProjectService.cs`
- `PortfolioClient.Wasm\Components\ProjectsSection.razor`
- `PortfolioClient.Wasm\wwwroot\css\app.css` (or dedicated CSS file)
- Create: `PortfolioClient.Wasm\wwwroot\images\projects\` (directory)

---

## 📦 Image Preparation Checklist

For each project, prepare the following:

### Screenshot Requirements:
- **Format:** PNG or WebP (WebP preferred for smaller file size)
- **Dimensions:** 800px width max (maintain aspect ratio)
- **File Size:** <200KB per image (use compression tools)
- **Quantity:** 2-3 screenshots per project
- **Content:** Show key features or UI highlights

### Screenshot Ideas per Project:
1. **Hockey Rink (SaaS Management System):**
   - User registration and session selection interface
   - Payment processing workflow
   - Administrative dashboard overview
   - League management and team creation

2. **Interactive Analytics Dashboard:**
   - Chart/graph examples
   - Data filtering interface
   - Report generation UI

3. **Accessible UI Components:**
   - Component library showcase
   - Accessibility features demo
   - Responsive design examples

4. **Developer Portfolio Dashboard:**
   - Homepage hero section
   - Projects section layout
   - Contact section

---

## 🚀 Implementation Priority

### Phase 1: Content Updates (Quick Wins)
1. ✅ Add SaaS to main landing page expertise
2. ✅ **COMPLETED** - Add Hockey Rink project details to StaticProjectService (replaced Healthcare Customer Portal)

### Phase 2: Visual Enhancements (Medium Effort)
1. ⏳ Implement screenshot layout in ProjectsSection
2. ⏳ Prepare and optimize project screenshots
3. ⏳ Add responsive CSS for image display

### Phase 3: Polish (Nice to Have)
1. ⏳ Add image lightbox/modal for full-size viewing
2. ⏳ Add image lazy loading optimization
3. ⏳ Add image carousel/slider for multiple screenshots

---

## 📝 Notes & Considerations

### Performance:
- Use lazy loading for images (`loading="lazy"`)
- Optimize images with tools like TinyPNG or WebP conversion
- Consider using responsive images (`srcset`) for different screen sizes

### Accessibility:
- Ensure all images have descriptive alt text
- Screenshot galleries should be keyboard navigable
- Consider adding image captions for context

### Mobile Experience:
- Screenshots should be scrollable horizontally on mobile
- Touch gestures for image viewing
- Ensure text remains readable on small screens

### Future Enhancements:
- Add filtering/categorization for projects
- Add "Featured" projects section
- Implement project search functionality
- Add project tags/categories for filtering

---

## 📞 Questions & Decisions Needed

Before implementing these changes, please provide:

### Hockey Rink Project:
- [ ] Project name/title
- [ ] Brief description (2-3 sentences)
- [ ] Technologies used
- [ ] 4-6 key features
- [ ] GitHub URL (if available)
- [ ] Live demo URL (if available)
- [ ] Project start/completion dates

### Screenshots:
- [ ] Do you have existing screenshots?
- [ ] Should screenshots be clickable for full-size view?
- [ ] Preferred image positioning (right, left, alternating)?
- [ ] Maximum number of screenshots per project?

### SaaS Expertise:
- [ ] Any specific SaaS projects to highlight?
- [ ] Should SaaS be a separate section or integrated?
- [ ] Any SaaS-specific achievements to mention?

---

## 📂 File Structure Reference

```
PortfolioClient.Wasm/
├── Components/
│   ├── Pages/
│   │   └── Home.razor                    # UPDATE: Add SaaS expertise
│   └── ProjectsSection.razor             # UPDATE: Add screenshot layout
├── Services/
│   └── StaticProjectService.cs          # ✅ UPDATED: Added Hockey Rink project, removed Healthcare Customer Portal
├── wwwroot/
│   ├── css/
│   │   └── app.css                      # UPDATE: Add screenshot styles
│   └── images/
│       └── projects/                    # CREATE: Project screenshots
│           ├── hockey-rink/             # NEW
│           ├── analytics-dashboard/
│           ├── accessible-components/
│           └── portfolio-dashboard/
└── ROADMAP.md                           # ✅ UPDATED: Hockey Rink project details added
```

---

## ✅ Completion Checklist

Use this checklist when implementing the enhancements:

### Content Updates:
- [ ] Update Home.razor with SaaS expertise
- [x] Add Hockey Rink project to StaticProjectService
- [ ] Update project descriptions if needed

### Visual Enhancements:
- [ ] Add Screenshots property to Project model
- [ ] Create images/projects directory structure
- [ ] Capture/gather project screenshots
- [ ] Optimize images for web
- [ ] Update ProjectsSection.razor layout
- [ ] Add CSS for screenshot display
- [ ] Test responsive design

### Quality Assurance:
- [ ] Test on multiple browsers (Chrome, Firefox, Safari, Edge)
- [ ] Test on mobile devices (iOS, Android)
- [ ] Verify image loading performance
- [ ] Check accessibility with screen reader
- [ ] Verify keyboard navigation
- [ ] Test with slow network connection
- [ ] Build and deploy to staging
- [ ] Final review and deploy to production

---

**Document Version:** 1.0  
**Created:** December 23, 2025  
**Last Updated:** December 23, 2025  
**Status:** Draft - Awaiting additional Hockey Rink project details
