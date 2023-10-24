<script setup lang="ts">
const { data: news, isLoading: newsIsLoading } = useNewsArticles()
</script>

<template>
  <div>
    <F7BlockTitle large>
      What's Happening
    </F7BlockTitle>

    <div class="space-y-3">
      <F7List v-if="newsIsLoading" inset>
        <div class="space-y-3">
          <F7SkeletonBlock v-for="n in 3" :key="n" class="rounded-md" effect="fade" height="10rem" />
        </div>
      </F7List>
      <template v-else-if="news">
        <span v-if="news.length === 0" class="mt-4 opacity-80">
          Nothing exciting is happening right now.
          <br>
          Check back later!
        </span>
        <F7Card v-for="article in news" :key="article.id" class="m-0!">
          <F7CardHeader class="h-44 rounded-[16px]! overflow-hidden bg-center bg-cover relative" valign="bottom" :style="`background-image: url(${article.heroImageUrl})`">
            <div class="absolute inset-0 bg-gradient-to-b from-transparent dark:to-black/70 to-white/70" />
            <span class="z-10 font-semibold">
              {{ article.title }}
            </span>
          </F7CardHeader>
          <F7CardContent>
            {{ article.description }}
          </F7CardContent>
          <F7CardFooter v-if="article.ctaTitle && article.ctaUrl">
            <F7Button tonal :href="article.ctaUrl" external target="_blank">
              {{ article.ctaTitle }}
            </F7Button>
          </F7CardFooter>
        </F7Card>
      </template>
    </div>
  </div>
</template>
