import dayjs from 'dayjs'
import RelativeTime from 'dayjs/plugin/relativeTime'

export const useDayjs = createSharedComposable(() => {
  dayjs.extend(RelativeTime)

  return dayjs
})
