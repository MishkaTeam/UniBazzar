import { createApp } from 'vue';
import Counter from './components/Counter.vue';
import PurchaseCounter from './components/purchase/PurchaseCounter.vue';

// لیستی از تمام کامپوننت‌های موجود
const components = {
  'Counter': Counter,
  'PurchaseCounter' : PurchaseCounter
};

function getPropsFromDataset(dataset) {
	const props = {};
	for (const key in dataset) {
		let value = dataset[key];
		// Try parsing numbers and booleans
		if (value === "true") value = true;
		else if (value === "false") value = false;
		else if (!isNaN(value)) value = Number(value);
		props[key] = value;
	}
	return props;

}
// پیدا کردن تمام المنت‌هایی که قرار است میزبان کامپوننت باشند
document.querySelectorAll('[data-vue-component]').forEach(el => {
  const componentName = el.dataset.vueComponent;
  const component = components[componentName];
  
	if (component) {
		const props = getPropsFromDataset(el.dataset);
		createApp(component, props).mount(el);
  } else {
    console.error(`کامپوننت '${componentName}' یافت نشد.`);
  }
});