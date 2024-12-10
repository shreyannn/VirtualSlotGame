<!-- Toast Notification Container -->
<div id="toast-container" class="fixed top-0 right-0 z-50 hidden space-y-2 mr-6 mt-6">
    <!-- Toast Example -->
    <div id="toast" class="hidden max-w-xs w-full p-4 bg-green-500 text-white rounded-lg shadow-lg flex items-center space-x-3 opacity-0 transition-opacity duration-300">
        <div class="flex-1">
            <span id="toast-message"></span>
        </div>
        <button id="toast-close" class="ml-3 text-white">
            <svg class="w-5 h-5" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="bi bi-x">
                <path fill-rule="evenodd" d="M10 8.586L5.707 4.293a1 1 0 1 0-1.414 1.414L8.586 10 4.293 14.293a1 1 0 1 0 1.414 1.414L10 11.414l4.293 4.293a1 1 0 1 0 1.414-1.414L11.414 10l4.293-4.293a1 1 0 1 0-1.414-1.414L10 8.586z"/>
            </svg>
        </button>
    </div>
</div>
<script>
    // Function to display the toast
    function showToast(message, type = 'green') {
        const toastContainer = document.getElementById('toast-container');
        const toast = document.getElementById('toast');
        const toastMessage = document.getElementById('toast-message');
        const toastCloseButton = document.getElementById('toast-close');

        // Set the message and style
        toastMessage.textContent = message;
        toast.classList.remove('hidden', 'opacity-0');
        toast.classList.add(`bg-${type}-500`);

        // Show toast container
        toastContainer.classList.remove('hidden');

        // Hide the toast after 3 seconds
        setTimeout(() => {
            toast.classList.add('opacity-0');
            setTimeout(() => {
                toastContainer.classList.add('hidden');
            }, 300);
        }, 3000);

        // Close the toast on button click
        toastCloseButton.addEventListener('click', () => {
            toast.classList.add('opacity-0');
            setTimeout(() => {
                toastContainer.classList.add('hidden');
            }, 300);
        });
    }

    // Display toast if session status exists
    @if(session('status'))
        showToast('{{ session('status') }}');
    @endif
</script>
