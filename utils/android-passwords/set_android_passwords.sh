#!/bin/bash

echo
echo "Change android keystore passwords in keychain"
echo
echo "Enter keystore password"
security add-generic-password -U -T "" -a unity_android_keystore_password -s unity_android_keystore -w

echo
echo "Enter keyalias password"
security add-generic-password -U -T "" -a unity_android_keyalias_password -s unity_android_keyalias -w

echo "keychain passwords changed!"
echo 