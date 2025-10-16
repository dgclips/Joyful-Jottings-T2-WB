// UnityFullscreenFix.jslib

mergeInto(LibraryManager.library, {
    EnterFullscreen: function () {
        try {
            var canvas = Module['canvas'];
            if (!canvas) {
                console.warn("Canvas not found");
                return;
            }

            // Inject fullscreen CSS dynamically if not already present
            if (!document.getElementById("fullscreen-ios-style")) {
                var style = document.createElement("style");
                style.id = "fullscreen-ios-style";
                style.innerHTML = `
                    canvas.fullscreen-ios {
                        position: fixed !important;
                        top: 0;
                        left: 0;
                        width: 100vw !important;
                        height: 100vh !important;
                        z-index: 9999 !important;
                        display: block !important;
                        max-width: 100% !important;
                        max-height: 100% !important;
                        touch-action: none !important;
                        margin: 0 !important;
                        padding: 0 !important;
                    }
                    body, html {
                        overscroll-behavior: none;
                        margin: 0 !important;
                        padding: 0 !important;
                    }
                `;
                document.head.appendChild(style);
            }

            // Apply fullscreen-safe styles
            document.body.style.overflow = 'hidden';
            document.body.style.position = 'fixed';
            document.documentElement.style.overflow = 'hidden';

            canvas.classList.add("fullscreen-ios");
            canvas.focus();

            var triggerReflow = function () {
                setTimeout(function () {
                    window.dispatchEvent(new Event('resize'));
                }, 100);
            };

            if (canvas.requestFullscreen) {
                canvas.requestFullscreen().then(triggerReflow).catch(function (err) {
                    console.warn("requestFullscreen failed:", err);
                });
            } else if (canvas.webkitRequestFullscreen) {
                canvas.webkitRequestFullscreen();
                triggerReflow();
            } else if (canvas.mozRequestFullScreen) {
                canvas.mozRequestFullScreen();
                triggerReflow();
            } else if (canvas.msRequestFullscreen) {
                canvas.msRequestFullscreen();
                triggerReflow();
            } else {
                console.warn("Fullscreen API not supported");
            }
        } catch (e) {
            console.warn("Failed to enter fullscreen:", e);
        }
    },

    ExitFullscreen: function () {
        try {
            var canvas = Module['canvas'];

            // Restore scroll behavior and remove class
            document.body.style.overflow = '';
            document.body.style.position = '';
            document.documentElement.style.overflow = '';
            if (canvas) {
                canvas.classList.remove("fullscreen-ios");
            }

            if (
                document.fullscreenElement || 
                document.webkitFullscreenElement || 
                document.mozFullScreenElement || 
                document.msFullscreenElement
            ) {
                if (document.exitFullscreen) {
                    document.exitFullscreen().catch(function (err) {
                        console.warn("exitFullscreen failed:", err);
                    });
                } else if (document.webkitExitFullscreen) {
                    document.webkitExitFullscreen();
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } else if (document.msExitFullscreen) {
                    document.msExitFullscreen();
                } else {
                    console.warn("Fullscreen exit not supported");
                }
            } else {
                console.log("Not in fullscreen, nothing to exit.");
            }
        } catch (e) {
            console.warn("Failed to exit fullscreen:", e);
        }
    },

    FocusUnityCanvas: function () {
        try {
            var canvas = Module['canvas'];
            if (canvas) {
                canvas.focus();
            } else {
                console.warn("Canvas not found");
            }
        } catch (e) {
            console.warn("Failed to focus canvas:", e);
        }
    }
});
