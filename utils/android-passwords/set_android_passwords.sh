#!/bin/bash


if [ "$#" -ne 1 ]
then
  echo "Usage: set_android_passwords.sh keystore_name"
  exit 1
fi

KEYSTORE_NAME=$1

echo
echo "Change android keystore passwords in keychain"
echo
echo "Enter keystore password"
security add-generic-password -U -T "" -a $KEYSTORE_NAME -s unity_android_keystore -w

echo
echo "Enter keyalias password"
security add-generic-password -U -T "" -a $KEYSTORE_NAME -s unity_android_keyalias -w

echo "keychain passwords changed!"
echo 