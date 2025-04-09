document.addEventListener('DOMContentLoaded', () => {

    const modalButton = document.querySelectorAll('[data-modal="true"]');
    modalButton.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);

            if (modal)
                modal.style.display = 'block';

            const closeOnOutsideClick = (event) => {
                if (!modal.querySelector('.modal-content').contains(event.target)) {
                    modal.style.display = 'none';
                    document.removeEventListener('click', closeOnOutsideClick);
                }
            };

            // Use setTimeout to avoid immediate closure on same click
            setTimeout(() => {
                document.addEventListener('click', closeOnOutsideClick);
            }, 0);
        })
    })
})