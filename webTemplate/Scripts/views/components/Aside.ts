((g) => {

	const signoutBtns = document.querySelectorAll('[data-fn="signout"]');

	signoutBtns.forEach(item => {
		item.addEventListener('click', HandleSignOutFn);
	})

	function HandleSignOutFn() {
		const Form = document.querySelector('#signoutForm');
		if (Form != null) {
			const _form = Form as HTMLFormElement;
			_form.submit();
		}
	}

})(window);