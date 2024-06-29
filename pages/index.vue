<script setup lang="ts">
useDark()
</script>

<template>
  <div>
    <header class="border-b border-b-solid border-b-gray-200 dark:border-b-gray-800 p-3">
      <div class="container mx-auto">
        <span class="font-semibold">SSTAA</span>
      </div>
    </header>

    <div class="p-3">
      <div class="container mx-auto flex flex-col gap-3">
        <NuxtLink to="/app" no-prefetch>
          <Button label="Open SSTAA App" class="w-full justify-start" />
        </NuxtLink>
        <NuxtLink to="/admin" no-prefetch>
          <Button label="Open SSTAA Admin Portal" class="w-full justify-start" />
        </NuxtLink>
        <NuxtLink to="/guard" no-prefetch>
          <Button label="Open SSTAA Guard House Portal" class="w-full justify-start" />
        </NuxtLink>
      </div>
    </div>
  </div>
</template>

<style>
html {
  font-family: sans-serif;
}

body {
  margin: 0;
}
</style>
