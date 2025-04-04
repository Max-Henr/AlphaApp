document.addEventListener('DOMContentLoaded', () => {

    const modalButton = document.querySelectorAll('[data-modal="true"]');
    modalButton.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);

            if (modal)
                modal.style.display = 'block';
        })
    })
})