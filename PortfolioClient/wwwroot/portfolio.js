// Portfolio page JavaScript functionality

// Store interval ID to prevent duplicates
let textRotationInterval = null;

// Initialize portfolio animations and features
window.initPortfolio = function() {
    console.log('initPortfolio called');
    
    // Clear any existing interval
    if (textRotationInterval) {
        console.log('Clearing existing interval');
        clearInterval(textRotationInterval);
        textRotationInterval = null;
    }

    // Animated text rotation
    const texts = [
        'digital experiences',
        'web applications',
        'user interfaces',
        'scalable solutions'
    ];
    let currentIndex = 0;
    
    // Try to find the element immediately
    let animatedTextElement = document.getElementById('animatedText');
    
    if (animatedTextElement) {
        console.log('Element found immediately, starting animation');
        startAnimation();
    } else {
        // If not found, wait a bit and try again
        console.log('Element not found, waiting...');
        setTimeout(() => {
            animatedTextElement = document.getElementById('animatedText');
            if (animatedTextElement) {
                console.log('Element found after delay, starting animation');
                startAnimation();
            } else {
                console.error('animatedText element still not found after delay');
            }
        }, 500);
    }
    
    function startAnimation() {
        if (!animatedTextElement) return;
        
        animatedTextElement.style.transition = 'opacity 0.3s ease';
        
        function rotateText() {
            if (!animatedTextElement) return;
            
            currentIndex = (currentIndex + 1) % texts.length;
            animatedTextElement.style.opacity = '0';
            
            setTimeout(() => {
                if (animatedTextElement) {
                    animatedTextElement.textContent = texts[currentIndex];
                    animatedTextElement.style.opacity = '1';
                    console.log('Text changed to: ' + texts[currentIndex]);
                }
            }, 300);
        }
        
        // Start the rotation
        textRotationInterval = setInterval(rotateText, 3000);
        console.log('Animation started, interval ID:', textRotationInterval);
    }
};

// Scroll to top utility function
window.scrollToTop = function() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
};

// Also initialize on DOMContentLoaded as backup
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', function() {
        console.log('DOMContentLoaded fired');
        setTimeout(() => window.initPortfolio(), 500);
    });
} else {
    console.log('Document already loaded');
}
