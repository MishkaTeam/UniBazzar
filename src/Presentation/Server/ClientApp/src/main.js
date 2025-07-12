import { createApp } from 'vue';
import Counter from './components/Counter.vue';

// لیستی از تمام کامپوننت‌های موجود
const components = {
  'Counter': Counter
};

// پیدا کردن تمام المنت‌هایی که قرار است میزبان کامپوننت باشند
document.querySelectorAll('[data-vue-component]').forEach(el => {
  const componentName = el.dataset.vueComponent;
  const component = components[componentName];
  
  if (component) {
    createApp(component).mount(el);
  } else {
    console.error(`کامپوننت '${componentName}' یافت نشد.`);
  }
});