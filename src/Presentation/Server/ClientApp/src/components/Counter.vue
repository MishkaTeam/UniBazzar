<template>
  <div class="card p-3 my-3">
    <h4>{{ message }}</h4>
    <div class="d-flex align-items-center">
      <button class="btn btn-primary" @click="count++">کلیک کن</button>
      <span class="ms-3 fs-5">تعداد: <strong>{{ count }}</strong></span>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const message = ref('این یک کامپوننت Vue است!')
const count = ref(0)
</script>

<style scoped>
.card {
  background-color: #f0f8ff;
  border: 1px solid #cce5ff;
}
</style>