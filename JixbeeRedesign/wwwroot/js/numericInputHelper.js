window.numericInputHelper = {
    setupFocusListeners: function (wrapper, dotnetRef) {
        const input = wrapper.querySelector("input");
        if (!input) return;

        input.addEventListener("focus", () => {
            dotnetRef.invokeMethodAsync("OnInputFocus");
        });

        input.addEventListener("blur", () => {
            dotnetRef.invokeMethodAsync("OnInputBlur");
        });
    },

    setupNumericInputFilter: function (wrapper) {
        const input = wrapper.querySelector("input");
        if (!input) return;

        input.addEventListener("input", () => {
            let value = input.value;

            // Remove all except digits and comma
            value = value.replace(/[^0-9,]/g, '');

            // Ensure only one comma
            const parts = value.split(',');
            if (parts.length > 2) {
                value = parts[0] + ',' + parts.slice(1).join('');
            }

            // Limit to 2 decimal digits
            if (parts.length === 2) {
                parts[1] = parts[1].substring(0, 2);
                value = parts[0] + ',' + parts[1];
            }

            input.value = value;

            // Trigger re-binding
            input.dispatchEvent(new Event('input', { bubbles: true }));
        });
    }
};