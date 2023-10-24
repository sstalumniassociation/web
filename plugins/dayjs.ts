import dayjs from 'dayjs'
import RelativeTime from 'dayjs/plugin/relativeTime'

export default defineNuxtPlugin(() => {
  dayjs.extend(RelativeTime)

  return {
    provide: {
      dayjs,
    },
  }
})
