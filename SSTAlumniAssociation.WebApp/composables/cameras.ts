import { useQuery } from '@tanstack/vue-query'
import { Html5Qrcode } from 'html5-qrcode'

export function useCameras() {
  return useQuery({
    queryKey: ['cameras'],
    queryFn: () => Html5Qrcode.getCameras(),
    refetchOnWindowFocus: false,
  })
}
