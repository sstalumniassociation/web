<script setup lang="ts">
import { signInWithEmailAndPassword } from 'firebase/auth'
import { f7, f7Button, f7List, f7ListInput, f7LoginScreen, f7LoginScreenTitle, f7Page } from 'framework7-vue'
import { useIsCurrentUserLoaded } from 'vuefire'
import { FirebaseError } from 'firebase/app'

const opened = defineModel<boolean>('opened')

const auth = useFirebaseAuth()
const authLoaded = useIsCurrentUserLoaded()
const { data: user, error, isLoading: userIsLoading } = useWhoAmI()

const state = reactive({
  email: '',
  password: '',
  pending: false,
})

async function login() {
  state.pending = true
  try {
    await signInWithEmailAndPassword(auth!, state.email, state.password)
  }
  catch (err) {
    if (err instanceof FirebaseError) {
      handleFirebaseError(err)
    }
    else {
      f7.toast.show({
        text: (err as Error).message,
        closeTimeout: 10000,
      })
    }
  }
  state.pending = false
}

const userIsGuardHouse = computed(() => {
  if (!authLoaded.value || userIsLoading.value)
    return null
  return user.value?.serviceAccount?.serviceAccountType === 'GuardHouse'
})

watch(userIsGuardHouse, (isGuardHouse) => {
  if (isGuardHouse) {
    opened.value = false
    return
  }

  console.log(user.value, error.value)

  f7.toast.show({
    text: 'Current user is not a service account.',
    closeTimeout: 10000,
  })
})

function handleFirebaseError(err: FirebaseError) {
  f7.toast.show({
    text: FIREBASE_ERROR_MESSAGES[err.code] ?? 'An unknown error occurred',
    closeTimeout: 10000,
  })
}
</script>

<template>
  <f7LoginScreen v-model:opened="opened">
    <f7Page login-screen>
      <f7LoginScreenTitle>Sign in to SSTAA Guard House</f7LoginScreenTitle>
      <f7List form @submit.prevent="login">
        <f7ListInput
          v-model:value="state.email" label="Email" type="email" placeholder="Your email address" validate
          required
        />

        <f7ListInput
          v-model:value="state.password" label="Password" type="password" placeholder="Password" validate
          required
        />

        <f7List inset>
          <f7Button type="submit" preloader :loading="state.pending" :disabled="state.pending">
            Login
          </f7Button>
        </f7List>
      </f7List>
    </f7Page>
  </f7LoginScreen>
</template>
