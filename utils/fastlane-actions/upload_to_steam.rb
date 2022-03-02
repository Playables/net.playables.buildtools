module Fastlane
  module Actions
    module SharedValues
      #UPLOAD_TO_STEAM_CUSTOM_VALUE = :UPLOAD_TO_STEAM_CUSTOM_VALUE
    end

    class UploadToSteamAction < Action
      def self.run(params)

        username = params[:username]
        steamcmd_path = params[:steamcmd_path]
        vdf_path = params[:vdf_path]

        cmd = [steamcmd_path,"+login",username,"+run_app_build",vdf_path,"+quit"]
        system(*cmd)
      end

      #####################################################
      # @!group Documentation
      #####################################################

      def self.description
        "You can use this action to upload to steam."
      end

      def self.details
        # Optional:
        # this is your chance to provide a more detailed description of this action
        "You can use this action to upload to steam."
      end

      def self.available_options
        # Define all options your action supports. 
        
        # Below a few examples
        [
          FastlaneCore::ConfigItem.new(key: :username,
                                       env_name: "FL_UPLOAD_TO_STEAM_USERNAME", # The name of the environment variable
                                       description: "username", # a short description of this parameter
                                       verify_block: proc do |value|
                                          UI.user_error!("No username set.") unless (value and not value.empty?)
                                       end),
          FastlaneCore::ConfigItem.new(key: :steamcmd_path,
                                       env_name: "FL_UPLOAD_TO_STEAM_STEAMCMD_PATH", # The name of the environment variable
                                       description: "path to steamcmd executable", # a short description of this parameter
                                       verify_block: proc do |value|
                                          UI.user_error!("No steamcmd path set.") unless (value and not value.empty?)
                                       end),
          FastlaneCore::ConfigItem.new(key: :vdf_path,
                                       env_name: "FL_UPLOAD_TO_STEAM_VDF_PATH",
                                       description: "VDF config path (relative to steamcmd)",
                                       verify_block: proc do |value|
                                          UI.user_error!("No steam vdf path set.") unless (value and not value.empty?)
                                       end) 
        ]
      end

      def self.output
        # Define the shared values you are going to provide
        # Example
        [
         # ['UPLOAD_TO_STEAM_CUSTOM_VALUE', 'A description of what this value contains']
        ]
      end

      def self.return_value
        # If your method provides a return value, you can describe here what it does
      end

      def self.authors
        # So no one will ever forget your contribution to fastlane :) You are awesome btw!
        ["Mario von Rickenbach"]
      end

      def self.is_supported?(platform)
        # you can do things like
        # 
        #  true
        # 
        #  platform == :ios
        # 
        #  [:ios, :mac].include?(platform)
        # 

        platform == :mac
      end
    end
  end
end
