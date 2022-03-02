module Fastlane
  module Actions
    module SharedValues
    end

    class CodesignMacAction < Action
      def self.run(params)

        certificate = params[:certificate]
        entitlements = params[:entitlements]
        target = params[:target]
 
        #sh("xattr","-d","attributename",target) # replace "attributename"

        UI.message "codesigning..."
        sh("codesign",
           "--force","-vvv","--options","runtime","--deep",
           "--sign",certificate,
           "--entitlements",entitlements,
           target)

        UI.message "verifying signature..."
        sh("codesign","--verify","--verbose=1",target)

      end

      #####################################################
      # @!group Documentation
      #####################################################

      def self.description
        "Code Sign macOS App"
      end

      def self.details
        "You can use this action to code sign a macOS app"
      end

      def self.available_options
        # Define all options your action supports.

        # Below a few examples
        [
          FastlaneCore::ConfigItem.new(key: :target,
                                       env_name: "FL_CODESIGN_TARGET", # The name of the environment variable
                                       description: "Target (eg. App Path)", # a short description of this parameter
                                       verify_block: proc do |value|
                                         UI.user_error!("Target not defined.") unless (value and not value.empty?)
          end),
          FastlaneCore::ConfigItem.new(key: :certificate,
                                       env_name: "FL_CODESIGN_CERTIFICATE", # The name of the environment variable
                                       description: "Certificate (find with 'security find-identity -p codesigning -v')", # a short description of this parameter
                                       verify_block: proc do |value|
                                         UI.user_error!("Certificate not defined.") unless (value and not value.empty?)
          end),
          FastlaneCore::ConfigItem.new(key: :entitlements,
                                       env_name: "FL_CODESIGN_ENTITLEMENTS", # The name of the environment variable
                                       description: "Entitlements", # a short description of this parameter
                                       verify_block: proc do |value|
                                         UI.user_error!("Entitlements id not defined.") unless (value and not value.empty?)
          end)
        ]
      end

      def self.output
        []
      end

      def self.return_value
      end

      def self.authors
        ["Mario von Rickenbach"]
      end

      def self.is_supported?(platform)

        [:mac].include?(platform)
      end
    end
  end
end
