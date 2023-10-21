<script setup lang="ts">
import { FirebaseError } from 'firebase/app'
import { FetchError } from 'ofetch'
import { useMutation } from '@tanstack/vue-query'
import { createUserWithEmailAndPassword, signInWithEmailAndPassword } from 'firebase/auth'

const opened = defineModel<boolean>()
const toast = useToast()

const auth = useFirebaseAuth()

const FIREBASE_ERROR_MESSAGES: Record<string, string> = {
  'auth/user-disabled': 'Your account has been temporarily disabled. Please try again later.',
  'auth/weak-password': 'Your password is too weak. Please use a stronger password',
  'auth/wrong-password': 'Your password is incorrect. Please try again.',
}

const state = reactive({
  email: '',
  password: '',
  confirmPassword: '',
  accountLinked: null as boolean | null,
  accountId: '',
  pending: false,
  formSubmitted: false,
})

watch(() => state.email, () => {
  if (state.accountLinked !== null)
    state.accountLinked = null
})

async function login() {
  state.formSubmitted = true

  state.pending = true

  if (state.accountLinked === null && state.email !== '')
    await validateEmail()
  else if (state.accountLinked === true && state.email !== '' && state.password !== '')
    await firebaseLogin()
  else if (state.accountLinked === false && state.email !== '' && state.password !== '' && state.confirmPassword !== '')
    await firebaseRegister()
  state.pending = false
}

const validateEmailMutation = useMutation({
  mutationFn: () => $api('/api/auth/verify', {
    method: 'POST',
    body: {
      email: state.email,
    },
  }),
})

async function validateEmail() {
  try {
    const account = await validateEmailMutation.mutateAsync()

    state.accountId = account.id
    state.accountLinked = account.linked
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

const firebaseRegisterMutation = useMutation({
  mutationFn: () => $api(`/api/user/${state.accountId}`, {
    method: 'POST',
    body: {
      firebaseId: auth!.currentUser!.uid,
    },
  }),
})

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
    firebaseRegisterMutation.mutate()
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
  <UModal v-model="opened" fullscreen>
    <div class="flex flex-col space-y-5 items-center mt-auto mb-auto">
      <h1 class="font-bold text-2xl">
        SSTAA Admin
      </h1>
      <UForm :state="state" class="w-1/3" @submit="login">
        <UFormGroup label="Email" name="email" required :error="(!state.email && state.formSubmitted) && 'Enter an email address!'">
          <UInput v-model="state.email" type="email" color="gray" variant="outline" placeholder="Your email address" />
        </UFormGroup>
        <template v-if="state.accountLinked !== null">
          <UFormGroup label="Password" name="password" required :error="(!state.password && state.formSubmitted) && 'Enter your password!'">
            <UInput v-model="state.password" type="password" color="gray" variant="outline" placeholder="Password" />
          </UFormGroup>
          <UFormGroup v-if="state.accountLinked === false" label="Confirm Password" name="confirmPassword" required :error="(!state.confirmPassword && state.formSubmitted) && 'Confirm your password!'">
            <UInput v-model="state.confirmPassword" type="password" color="gray" variant="outline" placeholder="Confirm Password" />
          </UFormGroup>
        </template>

        <UButton class="mt-3" type="submit" color="green" :loading="state.pending" :disabled="state.pending">
          {{ !state.accountLinked ? 'Continue' : 'Login' }}
        </UButton>
      </UForm>
    </div>
  </UModal>
</template>
