import { yupResolver } from '@hookform/resolvers/yup'
import Slider from '@react-native-community/slider'
import { nanoid } from '@reduxjs/toolkit'
import { useLocalSearchParams, router } from 'expo-router'
import Stack from 'expo-router/stack'
import { useState } from 'react'
import { useForm, useFieldArray } from 'react-hook-form'
import { View, Text, ScrollView, Pressable, TextInput } from 'react-native'

import { HandleResponse, Icons, SubmitModalBtn, TextField } from '@/components'
import { useCreateReviewMutation } from '@/services'
import { ratingStatus, reviewSchema } from '@/utils'
import { useTranslation } from 'react-i18next'

export default function ReviewCommentScreen() {
  //? Assets
  const { prdouctID, productTitle, numReviews } = useLocalSearchParams()
  const { t } = useTranslation();

  //? Refs
  const [positiveValue, setPositiveValue] = useState('')
  const [negativeValue, setNegativeValue] = useState('')

  //? State
  const [rating, setRating] = useState(5)

  //? Form Hook
  const {
    handleSubmit,
    register,
    formState: { errors: formErrors },
    reset,
    control,
  } = useForm({
    resolver: yupResolver(reviewSchema),
    defaultValues: {
      comment: '',
      title: '',
      positivePoints: [],
      negativePoints: [],
      rating: 1,
      product: '',
    },
  })

  const {
    fields: positivePointsFields,
    append: appentPositivePoint,
    remove: removePositivePoint,
  } = useFieldArray({
    name: 'positivePoints',
    control,
  })

  const {
    fields: negativePointsFields,
    append: appendNegativePoint,
    remove: removeNegativePoint,
  } = useFieldArray({
    name: 'negativePoints',
    control,
  })

  //? Create Review Query
  const [createReview, { isSuccess, isLoading, data, isError, error }] = useCreateReviewMutation()

  //? Handlers
  const handleAddPositivePoint = () => {
    if (positiveValue) {
      appentPositivePoint({ id: nanoid(), title: positiveValue })
      setPositiveValue('')
    }
  }

  const handleAddNegativePoint = () => {
    if (negativeValue) {
      appendNegativePoint({ id: nanoid(), title: negativeValue })
      setNegativeValue('')
    }
  }

  const submitHander = data =>
    createReview({
      body: { ...data, rating, product: prdouctID },
    })

  return (
    <>
      <Stack.Screen
        options={{
          title: `${t('comment')}, ${productTitle}`,
          headerBackTitleVisible: false,
        }}
      />
      {/* Handle Create Review Response */}
      {(isSuccess || isError) && (
        <HandleResponse
          isError={isError}
          isSuccess={isSuccess}
          error={error?.data?.message}
          message={data?.message}
          onSuccess={() => {
            reset()
            setRating(1)
            router.back()
          }}
          onError={() => { }}
        />
      )}
      <ScrollView className="bg-white">
        <View className="bg-white">
          <View className="flex flex-col justify-between flex-1 p-4 gap-y-5">
            {/* title */}
            <View
              className="w-full h-36 rounded-md bg-white p-5 items-center"
              style={{
                shadowColor: "#000",
                shadowOffset: {
                  width: 0,
                  height: 2,
                },
                shadowOpacity: 0.25,
                shadowRadius: 3.84,
                elevation: 5,
              }}
            >
              <Text className="text-lg">{t('your-feel')}</Text>
            </View>

            {/* positivePoints */}
            <View className="p-4">
              <View className="flex flex-row items-center justify-between border-b-[1px] border-gray-300 pb-2">
                <Text>{t('staff-attitude')}</Text>
                <View className="flex flex-row gap-x-4 items-center">
                  <Pressable className="active:scale-90">
                    <Icons.AntDesign name="like2" size={20} />
                  </Pressable>
                  <Pressable className="active:scale-90">
                    <Icons.AntDesign name="dislike2" size={20} />
                  </Pressable>
                </View>
              </View>

              <View className="flex flex-row items-center justify-between border-b-[1px] border-gray-300 pb-2 mt-6">
                <Text>{t('restaurant-service')}</Text>
                <View className="flex flex-row gap-x-4 items-center">
                  <Pressable className="active:scale-90">
                    <Icons.AntDesign name="like2" size={20} />
                  </Pressable>
                  <Pressable className="active:scale-90">
                    <Icons.AntDesign name="dislike2" size={20} />
                  </Pressable>
                </View>
              </View>

              <View className="flex flex-row items-center justify-between border-b-[1px] border-gray-300 pb-2 mt-6">
                <Text>{t('food-quality')}</Text>
                <View className="flex flex-row gap-x-4 items-center">
                  <Pressable className="active:scale-90">
                    <Icons.AntDesign name="like2" size={20} />
                  </Pressable>
                  <Pressable className="active:scale-90">
                    <Icons.AntDesign name="dislike2" size={20} />
                  </Pressable>
                </View>
              </View>

              <View className="flex flex-row items-center justify-between border-b-[1px] border-gray-300 pb-2 mt-6">
                <Text>{t('food-delivery')}</Text>
                <View className="flex flex-row gap-x-4 items-center">
                  <Pressable className="active:scale-90">
                    <Icons.AntDesign name="like2" size={20} />
                  </Pressable>
                  <Pressable className="active:scale-90">
                    <Icons.AntDesign name="dislike2" size={20} />
                  </Pressable>
                </View>
              </View>
            </View>

            {/* comment */}
            <View>
              <TextField
                label={t('write-comment')}
                control={control}
                errors={formErrors.comment}
                name="comment"
                multiline
                inputStyle={{ marginTop: 5, height: 120, textAlignVertical: "top" }}
              />
            </View>
            <View className="py-1">
              <SubmitModalBtn onPress={handleSubmit(submitHander)} isLoading={isLoading}>
                {t('send-comment')}
              </SubmitModalBtn>
            </View>
          </View>
        </View>
      </ScrollView>
    </>
  )
}
