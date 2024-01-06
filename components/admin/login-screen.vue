<script setup lang="ts">
import { FetchError } from 'ofetch'
import { createUserWithEmailAndPassword, signInWithEmailAndPassword } from 'firebase/auth'
import { FirebaseError } from 'firebase/app'

const opened = defineModel<boolean>('opened')

const toast = useToast()
const auth = useFirebaseAuth()

const state = reactive({
  email: '',
  password: '',
  confirmPassword: '',
  accountLinked: null as boolean | null,
  accountId: '',
  pending: false,
})

watch(() => state.email, () => {
  if (state.accountLinked !== null)
    state.accountLinked = null
})

async function login() {
  state.pending = true
  if (state.accountLinked === null)
    await validateEmail()
  else if (state.accountLinked === true)
    await firebaseLogin()
  else if (state.accountLinked === false)
    await firebaseRegister()
  state.pending = false
}

async function validateEmail() {
  try {
    const account = await $api('/api/auth/verify', {
      method: 'POST',
      body: {
        email: state.email,
      },
    })
    state.accountLinked = account.linked
    state.accountId = account.id
  }
  catch (err) {
    if (err instanceof FetchError && err.statusCode === 404) {
      toast.add({
        title: 'Please register with SSTAA',
        timeout: 10000,
      })
    }
    else {
      toast.add({
        title: (err as Error).message,
        timeout: 10000,
      })
    }
  }
}

async function firebaseLogin() {
  try {
    await signInWithEmailAndPassword(auth!, state.email, state.password)
  }
  catch (err) {
    if (err instanceof FirebaseError) {
      handleFirebaseError(err)
    }
    else {
      toast.add({
        title: (err as Error).message,
        timeout: 10000,
      })
    }
  }
}

async function firebaseRegister() {
  if (state.password !== state.confirmPassword) {
    toast.add({
      title: 'Your passwords do not match',
      timeout: 10000,
    })
    return
  }

  try {
    await createUserWithEmailAndPassword(auth!, state.email, state.password)
    await $api(`/api/user/${state.accountId}`, {
      method: 'POST',
      body: {
        firebaseId: auth!.currentUser!.uid,
      },
    })
  }
  catch (err) {
    if (err instanceof FirebaseError) {
      handleFirebaseError(err)
    }
    else {
      toast.add({
        title: (err as Error).message,
        timeout: 10000,
      })
    }
  }
}

function handleFirebaseError(err: FirebaseError) {
  toast.add({
    title: FIREBASE_ERROR_MESSAGES[err.code] ?? 'An unknown error occurred',
    timeout: 10000,
  })
}
</script>

<template>
  <UModal v-model="opened">
    <form @submit.prevent="login">
      <UCard>
        <template #header>
          <h1 class="font-semibold">
            Sign in to SSTAA
          </h1>
        </template>

        <div class="space-y-4">
          <UFormGroup label="Email">
            <UInput v-model="state.email" type="email" placeholder="Your email address" required />
          </UFormGroup>

          <template v-if="state.accountLinked !== null">
            <UFormGroup label="Password">
              <UInput v-model="state.password" type="password" placeholder="Your password" required />
            </UFormGroup>
            <UFormGroup v-if="state.accountLinked === false" label="Confirm password">
              <UInput
                v-model="state.confirmPassword" type="password"
                placeholder="Confirm password" required
              />
            </UFormGroup>
          </template>
        </div>

        <template #footer>
          <UButton type="submit" preloader :loading="state.pending">
            {{ !state.accountLinked ? 'Continue' : 'Login' }}
          </UButton>
        </template>
      </UCard>
    </form>
  </UModal>
</template>
