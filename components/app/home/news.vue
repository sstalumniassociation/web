<script setup lang="ts">
import { f7BlockTitle, f7Button, f7Card, f7CardContent, f7CardFooter, f7CardHeader, f7SkeletonBlock } from 'framework7-vue'

const { data: news, isLoading: newsIsLoading } = useNewsArticles()
</script>

<template>
  <div>
    <f7BlockTitle large>
      What's Happening
    </f7BlockTitle>

    <div class="space-y-3">
      <template v-if="newsIsLoading">
        <f7SkeletonBlock v-for="n in 3" :key="n" class="rounded-md" effect="fade" height="10rem" />
      </template>
      <template v-else-if="news">
        <span v-if="news.length === 0" class="mt-4 opacity-80">
          Nothing exciting is happening right now.
          <br>
          Check back later!
        </span>
        <f7Card v-for="article in news" :key="article.id" class="m-0!">
          <f7CardHeader class="h-45 rounded-[16px]! overflow-hidden bg-center bg-contain relative" valign="bottom" :style="`background-image: url(${article.heroImageUrl})`">
            <div class="absolute inset-0 bg-gradient-to-b from-transparent dark:to-black/70 to-white/70" />
            <span class="z-10 font-semibold">
              {{ article.title }}
            </span>
          </f7CardHeader>
          <f7CardContent>
            {{ article.description }}
          </f7CardContent>
          <f7CardFooter v-if="article.ctaTitle && article.ctaUrl">
            <f7Button tonal :href="article.ctaUrl" external target="_blank">
              {{ article.ctaTitle }}
            </f7Button>
          </f7CardFooter>
        </f7Card>
      </template>
    </div>
  </div>
</template>
